using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemyState : State
{
    [SerializeField] private NavigateEnemyState navigateState;
    public Transform target;
    public bool playerIsVisible;

    public override void Enter()
    {
        navigateState.isChasing = true;
        navigateState.destination = target.position;
        Set(navigateState,true);
    }

    public override void Do()
    {
        navigateState.destination = target.position;
        if(playerIsVisible)
        {
            isCompleted = navigateState.isCompleted;
        }
        else
        {
            isCompleted = true;
        }
        
    }
    public override void Exit()
    {
        navigateState.isChasing = false;
    }
}
