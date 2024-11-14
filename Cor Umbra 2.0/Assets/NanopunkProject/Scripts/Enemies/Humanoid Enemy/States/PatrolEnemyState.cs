using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemyState : State
{
    [SerializeField] private NavigateEnemyState navigateState;
    [SerializeField] private IdleEnemyState idleState;

    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask wallMask;
    [SerializeField] private Transform enemy;
    [SerializeField] private float walkRange;

    private float randonZ;
    private float randonX;
    private int timesRequested = 0;
    private Vector3 patrolDestination;

    public void GoToNextDestination()
    {
        timesRequested++;
        randonX = Random.Range(-walkRange, walkRange);
        randonZ = Random.Range(-walkRange, walkRange);

        patrolDestination = new Vector3(enemy.position.x + randonX, enemy.position.y, enemy.position.z + randonZ);

        if(Physics.Raycast(patrolDestination, -transform.up, 1f, groundMask) && !Physics.CheckSphere(patrolDestination, 1f, wallMask))
        {
            navigateState.destination = patrolDestination;
            Set(navigateState, true);
        }
        else if(timesRequested < 10)
        {
            GoToNextDestination();
        }
    }

    public override void Enter()
    {
        Set(idleState);
        isCompleted = true;
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
        else if(timesRequested < 50)
        {
            if(state.time > 5.2f)
            {
                GoToNextDestination();
            }
        }

    }

}
