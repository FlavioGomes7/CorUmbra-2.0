using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HittedState : State
{
    [SerializeField] PlayerController Player;

    public override void Enter()
    {
        Player.Hitbox.enabled = false;
        if (Player.CurrentHealth > 0)
        {
            animator.SetTrigger("Damaged");
        }
        //else if(Player.CurrentHealth <= 0)
        //{
        //    animator.applyRootMotion = true;
        //    animator.SetTrigger("Died");
        //}
    }
    public override void Do()
    {
        //if (Player.CurrentHealth > 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Damaged"))
        //{
        //    animator.SetTrigger("Damaged");
        //}
        if (Player.CurrentHealth <= 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
        {
            GameManager.instance.DisableEnemies();
            animator.applyRootMotion = true;
            animator.SetTrigger("Died");
        }

        if (time > 4f && Player.CurrentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (time > animator.GetCurrentAnimatorStateInfo(0).length && Player.CurrentHealth > 0)
        {
            isCompleted = true;
        }
    }
    public override void Exit()
    {
        Player.Hitbox.enabled = true;
        animator.applyRootMotion = false;
    }
}
