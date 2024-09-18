using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DieEnemyState : State
{
    [SerializeField] private NavMeshAgent agent;
    public override void Enter()
    {
        agent.isStopped = true;
        animator.applyRootMotion = true;
        animator.Play("Dying Mutant");
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
