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
        hitbox.enabled = true;
    }

    public override void Do()
    {
        if(time > 2.15f)
        {
            isCompleted = true;
        }
    }

    public override void Exit()
    {
        hitbox.enabled = false;
    }

    
}
