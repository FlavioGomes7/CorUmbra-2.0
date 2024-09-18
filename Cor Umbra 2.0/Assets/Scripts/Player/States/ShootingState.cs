using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingState : State
{
    public AimingState aiming;
    public IdleState idleState;
    public WalkingState walkingState;
    public float damage;

    private InputHandler inputHandler;

    public override void Enter()
    {
        inputHandler = InputHandler.instance;
        Set(idleState);
        if(aiming.hitTransform != null)
        {
            if(aiming.hitTransform.GetComponentInParent<HumanoidEnemy>() != null)
            {
                DealDamage(aiming.hitTransform.GetComponentInParent<HumanoidEnemy>(), damage, aiming.hitCollider);
            }
            else
            {
                Debug.Log("Acertou Algo");
            }
        }

    }
    public override void Do()
    {
        if(!inputHandler.shootTriggered)
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

    public void DealDamage(HumanoidEnemy target, float damage, Collider collider)
    {
        target.Hitted(collider, damage);
    }

}
