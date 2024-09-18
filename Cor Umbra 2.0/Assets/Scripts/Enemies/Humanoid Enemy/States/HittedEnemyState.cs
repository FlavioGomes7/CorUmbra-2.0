using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittedEnemyState : State
{
    [SerializeField] private HumanoidEnemy enemy;
    [SerializeField] private DieEnemyState dieState;

    public override void Enter()
    {
        if(enemy.health <= 0)
        {
            Set(dieState);
            isCompleted = state.isCompleted;
        }
        else
        {
            enemy.isHitted = false;
            isCompleted = true;
        }
    }
    public override void Do()
    {
       
    }
}
