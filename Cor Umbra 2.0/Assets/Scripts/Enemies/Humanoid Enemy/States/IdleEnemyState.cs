using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleEnemyState : State
{
    int idleTimes = 0;
    public override void Enter()
    {
        idleTimes++;
        isCompleted = true;
        animator.SetInteger("IdleTimes", idleTimes);
    }
    public override void Exit()
    {
        if(idleTimes >= 3)
        {
            idleTimes = 1;
        }
    }
}
