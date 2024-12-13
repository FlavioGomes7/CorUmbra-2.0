using UnityEngine;
using Cinemachine;
using UnityEngine.Animations.Rigging;


public class WalkingState : State
{
    private InputHandler inputHandler;
    [SerializeField] private CharacterController chController;
    [SerializeField] private Transform player;
    [SerializeField] private CinemachineFreeLook freeLook;
    [SerializeField] private bool isAimMode;
    [SerializeField] private Rig WalkingRig;

    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float sprintMultiplier;
    [SerializeField] private float turnSmoothTime;

    private Vector3 playerDirection;
    private float turnSmoothVelocity;


    public override void Enter()
    {
        inputHandler = InputHandler.instance;
        if(!isAimMode)
        {
            WalkingRig.weight = 1f; //Mathf.Lerp(WalkingRig.weight, 1, Time.deltaTime * 180);
        }
    }
    public override void Do()
    {
        if((inputHandler.sprintValue > 0 || inputHandler.moveInput.x != 0) && !isAimMode)
        {
            WalkingRig.weight = Mathf.Lerp(WalkingRig.weight, 0, Time.deltaTime * 100);
        }
        else if(WalkingRig.weight != 1.0f && inputHandler.sprintValue == 0 && !isAimMode)
        {
            WalkingRig.weight = Mathf.Lerp(WalkingRig.weight, 1, Time.deltaTime * 100);
        }

        float speed = playerSpeed * (inputHandler.sprintValue > 0 && inputHandler.moveInput.y > -0.5f ? sprintMultiplier : 1f);
        playerDirection = new Vector3(inputHandler.moveInput.x, 0f, inputHandler.moveInput.y).normalized;
        playerDirection = playerDirection.x * player.right + playerDirection.z * player.forward;

        if(!isAimMode)
        {                  
            float angle = Mathf.SmoothDampAngle(player.eulerAngles.y, freeLook.m_XAxis.Value, ref turnSmoothVelocity, turnSmoothTime);
            player.eulerAngles = new Vector3(player.localEulerAngles.x, angle, player.localEulerAngles.z);
        }

        playerDirection.y = -2;

        chController.Move(playerDirection * Time.deltaTime * speed);

        if(inputHandler.moveInput.magnitude == 0)
        {
            isCompleted = true;
        }
      
    }

    public override void Exit()
    {
 
    }
}
