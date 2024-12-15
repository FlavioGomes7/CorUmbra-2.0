using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittedEnemyState : State
{
    [SerializeField] private HumanoidEnemy enemy;
    [SerializeField] private DieEnemyState dieState;
    [SerializeField] private StunnedEnemyState stunnedState;

    [SerializeField] private Collider headcollider;

    public override void Enter()
    {
       
    }
    public override void Do()
    {
        if (state == stunnedState)
        {
            isCompleted = state.isCompleted;
            if (isCompleted)
            {
                enemy.isHitted = false;
            }
        }

        if (enemy.CurrentHealth <= 0)
        {
            Set(dieState);
            isCompleted = state.isCompleted;
        }
        else if (headcollider == enemy.hitCollider && enemy.CurrentHealth > 0)
        {
            Set(stunnedState);
        }
        else if(state != stunnedState)
        {
            enemy.isHitted = false;
            isCompleted = true;
        }

    }
    public override void Exit()
    {
        enemy.hitCollider = null;
        if(state != null)
        {
            Set(null);
        }
        animator.SetBool("IsStunned", false);
    }
}
