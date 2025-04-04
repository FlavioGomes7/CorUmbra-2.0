using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class IdleState : State
{
    [SerializeField] private Rig IdleRig;
    [SerializeField] private bool isChild;
    public override void Enter()
    {
        if (!isChild)
        {
            IdleRig.weight = 1f;
        }
        //Mathf.Lerp(IdleRig.weight, 1, Time.deltaTime * 180);
    }
    public override void Do()
    {
       
        isCompleted = true;
        
    }
    
}
