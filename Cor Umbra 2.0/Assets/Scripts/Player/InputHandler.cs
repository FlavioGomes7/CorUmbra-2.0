using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class InputHandler : MonoBehaviour
{

    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls;

    [Header("Action Map Name Reference")]
    [SerializeField] private string actionMapName = "Player";

    [Header("Action Name References")]
    [SerializeField] private string move = "Move";
    [SerializeField] private string sprint = "Sprint";
    [SerializeField] private string look = "Look";
    [SerializeField] private string aim = "Aim";
    [SerializeField] private string dash = "Dash";

    private InputAction moveAction;
    private InputAction sprintAction;
    private InputAction lookAction;
    private InputAction aimAction;
    private InputAction dashAction;

    public Vector2 moveInput { get; private set; }
    public Vector2 lookValue { get; private set;}
    public float sprintValue { get; private set; }
    public bool aimTriggered { get; private set; }
    public bool dashTriggered { get; private set; }

    public static InputHandler instance { get; private set; }


    private void RegisterInputActions()
    {
        moveAction.performed += context => moveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => moveInput = Vector2.zero;

        sprintAction.performed += context => sprintValue = context.ReadValue<float>();
        sprintAction.canceled += context => sprintValue = 0f; 


        aimAction.performed += context => aimTriggered = true;
        aimAction.canceled += context => aimTriggered = false;


        dashAction.performed += context => dashTriggered = true;
        dashAction.canceled += context => dashTriggered = false;
    }

    private void OnEnable()
    {
        moveAction.Enable();
        sprintAction.Enable();
        aimAction.Enable();
        dashAction.Enable();
    }
    private void OnDisable()
    {
        moveAction.Disable();
        sprintAction.Disable();
        aimAction.Disable();
        dashAction.Disable();
    }

    public void SwichInput(string action, bool isEnable)
    {
        InputAction inputAction = playerControls.FindActionMap(actionMapName).FindAction(action);
        if(isEnable == true)
        { inputAction.Enable(); }
        else
        { inputAction.Disable(); }
    }

    public IEnumerator Delay(float delay, string action)
    {
        InputAction inputAction = playerControls.FindActionMap(actionMapName).FindAction(action);
        inputAction.Disable();
        yield return new WaitForSeconds(delay);
        inputAction.Enable();
        StopCoroutine(Delay(delay, action));
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
     
        moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
        sprintAction = playerControls.FindActionMap(actionMapName).FindAction(sprint);
        aimAction = playerControls.FindActionMap(actionMapName).FindAction(aim);
        dashAction = playerControls.FindActionMap(actionMapName).FindAction(dash);
        RegisterInputActions();

    }


}
