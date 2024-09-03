using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardState : State
{
    public IdleState idleState;
    public WalkingState walkingState;
    public EvadeState evadeState;
    public AimingState aimingState;

    private InputHandler inputHandler;

    public override void Enter()
    {
        inputHandler = InputHandler.instance;
        Set(idleState);
    }
    public override void Do()
    {
        if (state.isCompleted)
        {
            Set(idleState);

            if (inputHandler.dashTriggered)
            {
                Set(evadeState);
            }
            else if (inputHandler.aimTriggered)
            {
                Set(aimingState);
            }
            else if (inputHandler.moveInput.magnitude > 0)
            {
                Set(walkingState);
            }
           
        }
        isCompleted = true;

    }

    public override void Exit()
    {
       
    }
}
