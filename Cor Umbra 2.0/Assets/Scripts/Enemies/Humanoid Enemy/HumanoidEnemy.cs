using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidEnemy : Core
{
    [SerializeField] PatrolEnemyState patrolState;
    [SerializeField] ChaseEnemyState chaseState;
    [SerializeField] AttackEnemyState attackState;
    [SerializeField] HittedEnemyState hittedState;

    public float health;

    [SerializeField] private Collider headCollider;
    [SerializeField] private Collider torsoCollider;
    [SerializeField] private Collider[] ArmLCollider;
    [SerializeField] private Collider[] ArmRCollider;
    [SerializeField] private Collider[] LegLCollider;
    [SerializeField] private Collider[] LegRCollider;

    [SerializeField] private Transform player;
    [SerializeField] private float sightRange;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask playerMask;

    private bool playerIsVisible;
    private bool playerInAttackRange;
    public bool isHitted = false;

    public void Start()
    {
        SetupInstances();
        Set(patrolState);
    }

    public void Update()
    {
        playerIsVisible = Physics.CheckSphere(transform.position, sightRange, playerMask);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);
        chaseState.playerIsVisible = playerIsVisible;
        SelectState();
    }

    public void Hitted(Collider collider, float damageReceived)
    {
        if (health > 0)
        {
            if (collider == headCollider)
            {
                health -= damageReceived * 2f;
            }
            else if (collider == torsoCollider)
            {
                health -= damageReceived * 1f;
            }
            else
            {
                health -= damageReceived * 0.8f;
            }
        }

        isHitted = true;
        Debug.Log(health);
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
                Set(attackState);
            }
            else if(!playerIsVisible && state.isCompleted)
            {
                Set(patrolState);
            }
        }

        state.DoBranch();
    }

}
