using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class StandardState : State
{
    public CinemachineFreeLook freeLook;
    public IdleState idleState;
    public WalkingState walkingState;
    public EvadeState evadeState;
    public AimingState aimingState;

    private InputHandler inputHandler;
    [SerializeField] private Rig IdleRig;
    [SerializeField] private Rig WalkingRig;


    public override void Enter()
    {
        inputHandler = InputHandler.instance;
        Set(idleState);
        freeLook.m_XAxis.m_MaxSpeed = 220f;
        freeLook.m_YAxis.m_MaxSpeed = 1f;
    }
    public override void Do()
    {
        if (state != idleState && IdleRig.weight != 0)
        {
            IdleRig.weight = 0f; //Mathf.Lerp(IdleRig.weight, 0, Time.deltaTime * 5);
        }

        if (state != walkingState && WalkingRig.weight != 0)
        {
            WalkingRig.weight = 0f; //Mathf.Lerp(WalkingRig.weight, 0, Time.deltaTime * 180);
        }

        if (inputHandler.dashTriggered)
        {
            Set(evadeState);
        }
        else if (inputHandler.aimTriggered && !evadeState.isStarted)
        {
            Set(aimingState);
        }
        else if (inputHandler.moveInput.magnitude > 0 && state.isCompleted)
        {
            Set(walkingState);
        }
        else if (inputHandler.moveInput.magnitude == 0 && state.isCompleted)
        {
            Set(idleState);
        }



        isCompleted = true;

    }

    public override void Exit()
    {
       
    }
}
