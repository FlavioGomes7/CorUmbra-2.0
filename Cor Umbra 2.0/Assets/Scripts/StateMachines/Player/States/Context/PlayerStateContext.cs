using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateContext
{
    private GameObject Player;
    private Transform FollowTarget;
    private CharacterController ChController;
    private InputHandler InputHandler;
    private Camera MainCamera;
    private CinemachineFreeLook FreeLookCamera;
    private CinemachineVirtualCamera VirtualCamera;

    private float PlayerSpeed;
    private float SprintMultiplier;
    private float TurnSmoothTime;

    private float Sensitivy;

    //Constructor
    public PlayerStateContext(GameObject player, Transform followTarget, CharacterController chController, InputHandler inputHandler, Camera mainCamera, CinemachineFreeLook freeLookCamera,
    CinemachineVirtualCamera virtualCamera, float playerSpeed, float sprintMultiplier, float turnSmoothTime, float sensitivy)
    {
        Player = player;
        FollowTarget = followTarget;
        ChController = chController;
        InputHandler = inputHandler;
        MainCamera = mainCamera;
        FreeLookCamera = freeLookCamera;
        VirtualCamera = virtualCamera;
        PlayerSpeed = playerSpeed;
        SprintMultiplier = sprintMultiplier;
        TurnSmoothTime = turnSmoothTime;
        Sensitivy = sensitivy;
    }

    //Getters
    public GameObject GetPlayer => Player;
    public Transform GetFollowTarget => FollowTarget;
    public CharacterController GetCharacterController => ChController;
    public InputHandler GetInputHandler => InputHandler;
    public Camera GetMainCamera => MainCamera;
    public CinemachineFreeLook GetFreeLookCamera => FreeLookCamera;
    public CinemachineVirtualCamera GetCinemachineVirtualCamera => VirtualCamera;
    public float GetPlayerSpeed => PlayerSpeed;
    public float GetSprintMultiplier => SprintMultiplier;
    public float GetTurnSmoothTime => TurnSmoothTime;
    public float GetSensitivy => Sensitivy;


}
