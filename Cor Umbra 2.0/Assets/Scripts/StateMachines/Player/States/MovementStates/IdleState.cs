using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MoveState
{
    public IdleState(PlayerStateContext ctx, MovementPlayerStateMachine.EMovementState eState) : base(ctx, eState)
    {
        PlayerStateContext Ctx = ctx;
    }

    public override void EnterState()
    {
        Debug.Log("Entrou No Estado Idle");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public override void UpdateState()
    {
        Debug.Log("Esta No Estado Idle");
    }
    public override void ExitState()
    {
        Debug.Log("Saiu do estado Idle");
    }

    public override MovementPlayerStateMachine.EMovementState GetNextState()
    {
        if (Ctx.GetInputHandler.moveInput.magnitude > 0)
        {
            return MovementPlayerStateMachine.EMovementState.Moving;
        }
        else if (Ctx.GetInputHandler.dashTriggered)
        {
            return MovementPlayerStateMachine.EMovementState.Dashing;
        }
        return MovementPlayerStateMachine.EMovementState.Idle;
    }


    public override void OnTriggerEnter(Collider other)
    {
      
    }

    public override void OnTriggerStay(Collider other)
    {
     
    }

    public override void OnTriggerExit(Collider other)
    {
     
    }
}
