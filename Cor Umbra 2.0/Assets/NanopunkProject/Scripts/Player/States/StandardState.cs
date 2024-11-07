using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class StandardState : State
{
    public CinemachineFreeLook freeLook;
    public IdleState idleState;
    public WalkingState walkingState;
    public EvadeState evadeState;
    public AimingState aimingState;

    private InputHandler inputHandler;

    public override void Enter()
    {
        inputHandler = InputHandler.instance;
        Set(idleState);
        freeLook.m_XAxis.m_MaxSpeed = 220f;
        freeLook.m_YAxis.m_MaxSpeed = 1f;
    }
    public override void Do()
    {


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
