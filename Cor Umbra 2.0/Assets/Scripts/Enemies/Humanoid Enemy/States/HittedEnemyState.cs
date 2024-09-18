using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittedEnemyState : State
{
    [SerializeField] private HumanoidEnemy enemy;
    [SerializeField] private DieEnemyState dieState;
    [SerializeField] private StunnedEnemyState stunnedState;

    [SerializeField] private Collider hitcollider;

    public override void Enter()
    {
       
    }
    public override void Do()
    {

        if (enemy.health <= 0)
        {
            Set(dieState);
            isCompleted = state.isCompleted;
        }
        else if (hitcollider == enemy.hitCollider && enemy.health > 0)
        {
            Set(stunnedState);
            isCompleted = state.isCompleted;
            if (isCompleted)
            {
                enemy.isHitted = false;
            }
        }
        else
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
