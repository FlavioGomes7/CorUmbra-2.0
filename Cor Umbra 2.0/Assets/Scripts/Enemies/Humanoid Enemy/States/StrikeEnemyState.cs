using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeEnemyState : State
{
    [SerializeField] private Transform enemy;
    [SerializeField] private Transform player;

    [SerializeField] private LayerMask playerMask;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private Collider hitbox;

    public override void Enter()
    {
        enemy.LookAt(player);
        animator.Play("Strike Mutant");
    }

    public override void Do()
    {
        if(time > 2.15f)
        {
            isCompleted = true;
        }

        ////if(Physics.CheckSphere(attackPoint.position, 0.5f, playerMask))
        ////{

        ////}
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (Physics.CheckSphere(attackPoint.position, 0.5f, playerMask) && collision.gameObject.GetComponent<PlayerController>() != null)
        {
            collision.gameObject.GetComponent<PlayerController>();
            Debug.Log("Acertou");
        }
    }
}
