using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayerStateMachine : StateHandle<MovementPlayerStateMachine.EMovementState>
{
    public enum EMovementState
    {
        Idle,
        Moving,
        Dashing,
    }

    private PlayerStateContext Ctx;

    //Components
    [SerializeField] private GameObject Player;
    [SerializeField] private Transform FollowTarget;
    [SerializeField] private InputHandler InputHandler;
    [SerializeField] private CharacterController ChController;
    [SerializeField] private Camera MainCamera;
    [SerializeField] private CinemachineFreeLook FreeLookCamera;
    [SerializeField] private CinemachineVirtualCamera VirtualCamera;

    [SerializeField] private float PlayerSpeed;
    [SerializeField] private float SprintMultiplier;
    [SerializeField] private float TurnSmoothTime;

    [SerializeField] private float Sensitivy;

    private void Awake()
    {
        Ctx = new PlayerStateContext(Player, FollowTarget, ChController, InputHandler, MainCamera, FreeLookCamera, VirtualCamera, PlayerSpeed, SprintMultiplier,
        TurnSmoothTime, Sensitivy);
        InitializeStates();
    }

    private void InitializeStates()
    {
        States.Add(EMovementState.Idle, new IdleState(Ctx, EMovementState.Idle));
        States.Add(EMovementState.Moving, new MovingState(Ctx, EMovementState.Moving));
        States.Add(EMovementState.Dashing, new DashingState(Ctx, EMovementState.Dashing));

        CurrentState = States[EMovementState.Idle];
    }
        

}
