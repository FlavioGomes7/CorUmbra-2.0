using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackEnemyState : State
{
    [SerializeField] private StrikeEnemyState strikeState;
    [SerializeField] private NavMeshAgent Enemy;
    public override void Enter()
    {
        Enemy.isStopped = true;
        Set(strikeState, true);
    }
    public override void Do()
    {
        if(state != null)
        {
            if(state.isCompleted)
            {
                Set(null);
            }
        }
        else
        {
            isCompleted = true;
        }
              
    }
}
