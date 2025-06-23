using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeEnemyState : State
{
    [SerializeField] private Transform enemy;
    [SerializeField] private Transform player;

    [SerializeField] private LayerMask playerMask;

    [SerializeField] private GameObject hitbox;

    public override void Enter()
    {
        //hitbox.SetActive(true);
        enemy.LookAt(player);
        animator.Play("Strike Mutant");
        //Debug.Log(hitbox.activeSelf);
    }

    public override void Do()
    {
        //if(time > 2.15f)
        //{
        //    isCompleted = true;
        //}
        if(time > 1.0f)
        {
            hitbox.SetActive(true);
        }
        if (time >= animator.GetCurrentAnimatorStateInfo(0).length)
        {
            isCompleted = true;
        }
    }

    public override void Exit()
    {
        hitbox.SetActive(false);
        //Debug.Log(hitbox.activeSelf);
    }

    
}
