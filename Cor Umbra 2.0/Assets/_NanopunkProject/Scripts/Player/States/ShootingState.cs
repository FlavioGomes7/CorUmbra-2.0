using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShootingState : State
{
    [SerializeField] private PlayerWeaponSelector weaponSelector;
    [SerializeField] private PlayerController playerController;

    public AimingState aiming;
    public IdleState idleState;
    public WalkingState walkingState;
    //public float damage;

    private InputHandler inputHandler;

    public override void Enter()
    {
        inputHandler = InputHandler.instance;
        Set(idleState);
        weaponSelector.ActiveWeapon.Shoot();
        playerController.UpdateTextAmmo();
        //if(weaponSelector.ActiveWeapon.hitCollider != null)
        //{
        //    if(weaponSelector.ActiveWeapon.hitTransform.GetComponentInParent<HumanoidEnemy>() != null)
        //    {
        //        Debug.Log("Acertou o inimigo");
        //        //DealDamage(weaponSelector.ActiveWeapon.hitTransform.GetComponentInParent<HumanoidEnemy>(), damage, weaponSelector.ActiveWeapon.hitCollider);
        //    }
        //    else
        //    {
        //        Debug.Log("Acertou Algo");
        //    }
        //}

    }
    public override void Do()
    {
        //weaponSelector.ActiveWeapon.Shoot();

        if (!inputHandler.shootTriggered)
        {
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
       
    }

    //public void DealDamage(HumanoidEnemy target, float damage, Collider collider)
    //{
    //    target.Hitted(collider, damage);
    //}

}
