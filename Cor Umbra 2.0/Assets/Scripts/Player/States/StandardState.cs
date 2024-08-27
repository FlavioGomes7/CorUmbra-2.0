using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardState : State
{
    public IdleState idleState;
    public WalkingState walkingState;
    public EvadeState evadeState;

    private InputHandler inputHandler;

    public override void Enter()
    {
        inputHandler = InputHandler.instance;
        Set(idleState);
    }
    public override void Do()
    {
        if(state.isCompleted)
        {
            if(inputHandler.moveInput.magnitude > 0)
            {
                Set(walkingState);
            }  
            else
            {
                Set(idleState);
            }
        }
        else if(inputHandler.dashTriggered)
        {
            Set(evadeState);
        } 
       
    }

    public override void Exit()
    {

    }
}
