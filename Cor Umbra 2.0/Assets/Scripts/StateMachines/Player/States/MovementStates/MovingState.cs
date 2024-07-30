using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class MovingState : MoveState
{
    private Vector3 PlayerDirection;
    private float turnSmoothVelocity;
    public MovingState(PlayerStateContext ctx, MovementPlayerStateMachine.EMovementState eState) : base(ctx, eState)
    {
        PlayerStateContext Ctx = ctx;
    }

    public override void EnterState()
    {
        Debug.Log("Entrou No Estado Moving");
    }
    public override void UpdateState()
    {
        HandleMovement();
    }
    public override void ExitState()
    {
        Debug.Log("Saiu do estado Moving");
    }

    public override MovementPlayerStateMachine.EMovementState GetNextState()
    {
        if(Ctx.GetInputHandler.moveInput.magnitude == 0)
        {
            return MovementPlayerStateMachine.EMovementState.Idle;
        }
        else if(Ctx.GetInputHandler.dashTriggered)
        {
            return MovementPlayerStateMachine.EMovementState.Dashing;
        }
        return MovementPlayerStateMachine.EMovementState.Moving;
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

    private void HandleMovement()
    {
        float speed = Ctx.GetPlayerSpeed * (Ctx.GetInputHandler.sprintValue > 0 && Ctx.GetInputHandler.moveInput.y > -0.5f ? Ctx.GetSprintMultiplier : 1f);
        PlayerDirection = new Vector3(Ctx.GetInputHandler.moveInput.x, 0f, Ctx.GetInputHandler.moveInput.y).normalized;
        PlayerDirection = PlayerDirection.x * Ctx.GetPlayer.transform.right + PlayerDirection.z * Ctx.GetPlayer.transform.forward;
        float angle = Mathf.SmoothDampAngle(Ctx.GetPlayer.transform.eulerAngles.y, Ctx.GetFreeLookCamera.m_XAxis.Value, ref turnSmoothVelocity, Ctx.GetTurnSmoothTime);

        Ctx.GetCharacterController.Move(PlayerDirection * Time.deltaTime * speed);
        Ctx.GetPlayer.transform.eulerAngles = new Vector3(Ctx.GetPlayer.transform.localEulerAngles.x, angle, Ctx.GetPlayer.transform.localEulerAngles.z);
        Ctx.GetFollowTarget.transform.position = new Vector3(Ctx.GetPlayer.transform.position.x, Ctx.GetFollowTarget.transform.position.y, Ctx.GetPlayer.transform.position.z);
    }
}
