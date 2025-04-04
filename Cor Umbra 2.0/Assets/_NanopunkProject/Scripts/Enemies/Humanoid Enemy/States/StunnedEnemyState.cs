using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StunnedEnemyState : State
{
    [SerializeField] private HumanoidEnemy enemy;
    [SerializeField] private NavMeshAgent agent;
    public override void Enter()
    {
        agent.isStopped = true;
        animator.SetBool("IsStunned", true);
    }
    public override void Do()
    {
        if(time >= animator.GetCurrentAnimatorStateInfo(0).length)
        {
            isCompleted = true;
        }
      
       
    }
    public override void Exit()
    {
        agent.isStopped = false;
    }
}
