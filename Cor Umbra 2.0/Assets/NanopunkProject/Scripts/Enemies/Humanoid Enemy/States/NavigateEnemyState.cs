using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NavigateEnemyState : State
{
    [SerializeField] private Transform enemy;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float speed;
    [SerializeField] private float sprintValue;
    private Vector3 distanceToDestinantion;
    public Vector3 destination;
    public bool isChasing;

    public override void Enter()
    {
        agent.isStopped = false;
        agent.speed = speed;
        agent.SetDestination(destination);
        animator.SetBool("IsMoving", true);
        if(isChasing)
        {
            agent.speed = speed * sprintValue;
            animator.SetBool("IsRunning", true);
        }
        
      
    }
    public override void Do()
    {
        enemy.LookAt(destination);
        distanceToDestinantion = enemy.position - destination;
        if(isChasing)
        {
            agent.SetDestination(destination);
        }
        else if(distanceToDestinantion.magnitude < 1f)
        {
            isCompleted = true;
        }
    }
    public override void Exit()
    {
        agent.isStopped = true;
        animator.SetBool("IsMoving", false);
        animator.SetBool("IsRunning", false);    
    }


}
