using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ReloadingState : State
{
    [SerializeField] PlayerController playerController;
    [SerializeField] private PlayerWeaponSelector weaponSelector;
    public IdleState idleState;
    public WalkingState walkingState;
    private InputHandler inputHandler;

    [SerializeField] private Rig IdleRig;
    public override void Enter()
    {
        IdleRig.weight = 0f;
        animator.SetLayerWeight(3, 1);
        inputHandler = InputHandler.instance;
        //weaponSelector.ActiveWeapon.AddAmmo();
        animator.Play("Reload", 3);
        //weaponSelector.ActiveWeapon.Reload();  
    }
    public override void Do()
    {
       
        if (time > animator.GetCurrentAnimatorStateInfo(3).length)
        {
            weaponSelector.ActiveWeapon.Reload();
            isCompleted = true;
        }
        if (inputHandler.moveInput.magnitude > 0)
        {
            Set(walkingState);
        }
        else
        {
            Set(idleState);
        }
    }

    public override void Exit()
    {
        playerController.UpdateTextAmmo();
        animator.SetLayerWeight(3, 0);
    }
}
