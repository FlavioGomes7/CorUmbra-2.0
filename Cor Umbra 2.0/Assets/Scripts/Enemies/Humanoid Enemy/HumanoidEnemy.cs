using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidEnemy : Core
{
    [SerializeField] PatrolEnemyState patrolState;
    [SerializeField] ChaseEnemyState chaseState;
    [SerializeField] AttackEnemyState attackState;
    [SerializeField] HittedEnemyState hittedState;

    [SerializeField] private Transform player;
    [SerializeField] private float sightRange;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask playerMask;

    private bool playerIsVisible;
    private bool playerInAttackRange;

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

    private void SelectState()
    {
        if(groundSensor.grounded)
        {
            if(playerIsVisible && !playerInAttackRange && state.isCompleted)
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
