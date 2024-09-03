using UnityEngine;
using Cinemachine;


public class WalkingState : State
{
    private InputHandler inputHandler;
    [SerializeField] private CharacterController chController;
    [SerializeField] private Transform player;
    [SerializeField] private CinemachineFreeLook freeLook;
    [SerializeField] private bool isAimMode;

    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float sprintMultiplier;
    [SerializeField] private float turnSmoothTime;

    private Vector3 playerDirection;
    private float turnSmoothVelocity;


    public override void Enter()
    {
        isCompleted = true;
        inputHandler = InputHandler.instance;
    }
    public override void Do()
    {
        float speed = playerSpeed * (inputHandler.sprintValue > 0 && inputHandler.moveInput.y > -0.5f ? sprintMultiplier : 1f);
        playerDirection = new Vector3(inputHandler.moveInput.x, 0f, inputHandler.moveInput.y).normalized;
        playerDirection = playerDirection.x * player.right + playerDirection.z * player.forward;

        if(!isAimMode)
        {      
            float angle = Mathf.SmoothDampAngle(player.eulerAngles.y, freeLook.m_XAxis.Value, ref turnSmoothVelocity, turnSmoothTime);
            player.eulerAngles = new Vector3(player.localEulerAngles.x, angle, player.localEulerAngles.z);
        }

        chController.Move(playerDirection * Time.deltaTime * speed);
      
    }

    public override void Exit()
    {
 
    }
}
