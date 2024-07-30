using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MovementPlayerStateMachine;

public class PlayerCombatStateMachine : StateHandle<PlayerCombatStateMachine.ECombatState>
{
    public enum ECombatState
    {
        Standard,
        Aiming,
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
        States.Add(ECombatState.Standard, new StandardState(Ctx, ECombatState.Standard));
        States.Add(ECombatState.Aiming, new AimingState(Ctx, ECombatState.Aiming));

        CurrentState = States[ECombatState.Standard];
    }
}
