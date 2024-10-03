using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingState : State
{
    public IdleState idleState;
    public WalkingState walkingState;
    public ShootingState shotingState;

    [SerializeField] private Transform player;
    [SerializeField] private Transform playerAim;
    [SerializeField] public float sensitivy;
    [SerializeField] private CinemachineVirtualCamera aimCamera;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private Transform debugTransform;
    [SerializeField] private LayerMask aimColliderMask = new LayerMask();

    private InputHandler inputHandler;
    public Collider hitCollider = null;
    public Transform hitTransform = null;

    private float rotationY;

    public override void Enter()
    {
        inputHandler = InputHandler.instance;
        aimCamera.Priority = 2;
        crosshair.SetActive(true);
    }

    public override void Do()
    {
        HandleAim();

        if(inputHandler.shootTriggered)
        {
            Set(shotingState);
        }
        else if(inputHandler.moveInput.magnitude > 0)
        {
            Set(walkingState);
        }
        else
        {
            Set(idleState);
        }


        if (!inputHandler.aimTriggered)
        {
            isCompleted = true;
        }

    }

    public override void Exit()
    {
        aimCamera.Priority = 0;
        crosshair.SetActive(false);
    }

    void HandleAim()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
       
        player.rotation *= Quaternion.AngleAxis(inputHandler.lookValue.x * sensitivy, Vector3.up);
        playerAim.rotation *= Quaternion.AngleAxis(-inputHandler.lookValue.y * sensitivy, Vector3.right);

        var angles = player.eulerAngles;
        angles.z = 0;

       // var angle = playerAim.eulerAngles;

        //if(angle.x > 180 && angle.x < 340)
        //{
        //    angle.x = 340;
        //}
        //else if(angle.x < 180 && angle.x > 40)
        //{
        //    angle.x = 40;
        //}

        //playerAim.localEulerAngles = angle;
        player.transform.localEulerAngles = angles;

        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.PositiveInfinity, aimColliderMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
            hitTransform = raycastHit.transform;
            hitCollider = raycastHit.collider;
        }

        Vector3 worldAimTarget = mouseWorldPosition;
        worldAimTarget.y = player.position.y;
        Vector3 aimDirection = (worldAimTarget - player.position).normalized;

        //player.forward = Vector3.Lerp(player.forward, aimDirection, Time.deltaTime * 20f);
    }
}
