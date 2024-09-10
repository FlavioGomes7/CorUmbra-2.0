using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemyState : State
{
    [SerializeField] private NavigateEnemyState navigateState;
    [SerializeField] private IdleEnemyState idleState;

    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform enemy;
    [SerializeField] private float walkRange;

    private float randonZ;
    private float randonX;
    private Vector3 patrolDestination;

    public void GoToNextDestination()
    {
        randonX = Random.Range(-walkRange, walkRange);
        randonZ = Random.Range(-walkRange, walkRange);

        patrolDestination = new Vector3(enemy.position.x + randonX, enemy.position.y, enemy.position.z + randonZ);

        if(Physics.Raycast(patrolDestination, -transform.up, 1f, groundMask))
        {
            navigateState.destination = patrolDestination;
            Set(navigateState, true);
        }
        else
        {
            GoToNextDestination();
        }
    }

    public override void Enter()
    {
        GoToNextDestination();
    }

    public override void Do()
    {

        if(state == navigateState)
        {
            if(navigateState.isCompleted)
            {
                Set(idleState, true);
            }
        }
        else
        {
            if(state.time > 5.2f)
            {
                GoToNextDestination();
            }
        }

    }

}
