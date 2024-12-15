using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class DieEnemyState : State
{
    [SerializeField] private HumanoidEnemy Enemy;
    [SerializeField] private NavMeshAgent agent;
    public override void Enter()
    {
        agent.isStopped = true;
        animator.applyRootMotion = true;
        animator.Play("Dying Mutant");
    }
    public override void Do()
    {
        Enemy.headCollider.enabled = false;
        Enemy.torsoCollider.enabled = false;

        foreach(var collider in Enemy.ArmLCollider)
        {
            collider.enabled = false;
        }
        foreach (var collider in Enemy.ArmRCollider)
        {
            collider.enabled = false;
        }
        foreach (var collider in Enemy.LegRCollider)
        {
            collider.enabled = false;
        }
        foreach (var collider in Enemy.LegLCollider)
        {
            collider.enabled = false;
        }

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
