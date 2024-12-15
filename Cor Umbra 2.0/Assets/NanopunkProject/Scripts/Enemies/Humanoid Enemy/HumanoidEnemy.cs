using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidEnemy : Core, IDamageable
{
    [SerializeField] PatrolEnemyState patrolState;
    [SerializeField] ChaseEnemyState chaseState;
    [SerializeField] AttackEnemyState attackState;
    [SerializeField] HittedEnemyState hittedState;

    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] public float strikeDamage;

    [SerializeField] public Collider headCollider;
    [SerializeField] public Collider torsoCollider;
    [SerializeField] public Collider[] ArmLCollider;
    [SerializeField] public Collider[] ArmRCollider;
    [SerializeField] public Collider[] LegLCollider;
    [SerializeField] public Collider[] LegRCollider;

    [SerializeField] private Transform player;
    [SerializeField] private float sightRange;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask playerMask;

    private bool playerIsVisible;
    private bool playerInAttackRange;
    public bool isHitted = false;
    public Collider hitCollider;

    public event IDamageable.TakeDamageEvent OnTakeDamage;
    public event IDamageable.DeathEvent OnDeath;

    public float CurrentHealth { get => currentHealth; private set => currentHealth = value; }

    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }

    public void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public void Start()
    {
        SetupInstances();
        playerIsVisible = false;
        playerInAttackRange = false;
        Set(patrolState);
    }

    public void Update()
    {
        playerIsVisible = Physics.CheckSphere(transform.position, sightRange, playerMask);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);
        chaseState.playerIsVisible = playerIsVisible;
        SelectState();
    }

    private void SelectState()
    {
        if(groundSensor.grounded)
        {
            if(isHitted)
            {
                Set(hittedState);
            }
            else if(playerIsVisible && !playerInAttackRange && state.isCompleted)
            {
                chaseState.target = player;
                Set(chaseState);                
            }
            else if(playerIsVisible && playerInAttackRange)
            {
                Set(attackState,true);
            }
            else if(!playerIsVisible && state.isCompleted)
            {
                Set(patrolState);
            }
        }

        state.DoBranch();
    }

    public void TakeDamage(float damage, Collider collider)
    {
        float damageTaken = Mathf.Clamp(damage, 0, currentHealth);
        hitCollider = collider;

        if (collider == headCollider)
        {
            CurrentHealth -= damageTaken * 2f;
        }
        else if (collider == torsoCollider)
        {
            CurrentHealth -= damageTaken * 1f;
        }
        else
        {
            CurrentHealth -= damageTaken * 0.8f;
        }
        isHitted = true;

        if(damageTaken != 0)
        {
            OnTakeDamage?.Invoke(damageTaken);
        }

        if(CurrentHealth == 0 && damageTaken != 0)
        {
            OnDeath?.Invoke();
        }
        

    }
}
