using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingState : State
{
    public AimingState aiming;

    public override void Enter()
    {
       if (aiming.hitTransform != null)
       {
            if(aiming.hitTransform.GetComponent<TargetScript>() != null)
            {
                Debug.Log("Acertou o Alvo");
            }
            else
            {
                Debug.Log("Acertou Algo");
            }
       }

    }
    public override void Do()
    {
       isCompleted = true;
    }

}
