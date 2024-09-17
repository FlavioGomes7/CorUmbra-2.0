using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackEnemyState : State
{
    [SerializeField] private StrikeEnemyState strikeState;
    public override void Enter()
    {
        Set(strikeState, true);
    }
    public override void Do()
    {
        if(state != null)
        {
            isCompleted = state.isCompleted;
        }
        else
        {
            isCompleted = true;
        }
              
    }
}
