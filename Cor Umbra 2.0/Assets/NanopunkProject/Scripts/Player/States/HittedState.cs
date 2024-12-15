using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittedState : State
{
    [SerializeField] PlayerController Player;

    public override void Enter()
    {
        if(Player.CurrentHealth > 0)
        {
            animator.SetTrigger("Damaged");
        }
        else if(Player.CurrentHealth <= 0)
        {
            animator.applyRootMotion = true;
            animator.SetTrigger("Died");
        }
    }
    public override void Do()
    {
        if (time >= animator.GetCurrentAnimatorStateInfo(0).length)
        {
            isCompleted = true;
        }
    }
    public override void Exit()
    {
        animator.applyRootMotion = false;
    }
}
