using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittedState : State
{
    public override void Enter()
    {
        animator.SetTrigger("Damaged");
    }
}
