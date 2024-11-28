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
    [SerializeField] private string shoot = "Shoot";
    [SerializeField] private string dash = "Dash";
    [SerializeField] private string pick = "Pick";
    [SerializeField] private string settings = "Settings";
    [SerializeField] private string restart = "Restart";

    private InputAction moveAction;
    private InputAction sprintAction;
    private InputAction lookAction;
    private InputAction aimAction;
    private InputAction shootAction;
    private InputAction dashAction;
    private InputAction pickAction;
    private InputAction settingsAction;
    private InputAction restartAction;

    public Vector2 moveInput { get; private set; }
    public Vector2 lookValue { get; private set;}
    public float sprintValue { get; private set; }
    public bool aimTriggered { get; private set; }
    public bool shootTriggered { get; private set; }
    public bool dashTriggered { get; private set; }
    public bool pickTriggered { get; private set; }
    public bool settingsTriggered { get; private set; }
    public bool restartTriggered { get; private set; }

    public static InputHandler instance { get; private set; }


    private void RegisterInputActions()
    {
        moveAction.performed += context => moveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => moveInput = Vector2.zero;

        sprintAction.performed += context => sprintValue = context.ReadValue<float>();
        sprintAction.canceled += context => sprintValue = 0f;

        lookAction.performed += context => lookValue = context.ReadValue<Vector2>();
        lookAction.canceled += context => lookValue = Vector2.zero;

        aimAction.performed += context => aimTriggered = true;
        aimAction.canceled += context => aimTriggered = false;

        shootAction.performed += context => shootTriggered = true;
        shootAction.canceled += context => shootTriggered = false;

        dashAction.performed += context => dashTriggered = true;
        dashAction.canceled += context => dashTriggered = false;

        pickAction.performed += context => pickTriggered = true;
        pickAction.canceled += context => pickTriggered = false;

        settingsAction.performed += context => settingsTriggered = true;
        settingsAction.canceled += context => settingsTriggered = false;

        restartAction.performed += context => restartTriggered = true;
        restartAction.canceled += context => restartTriggered = false;
    }

    private void ShootAction_canceled(InputAction.CallbackContext obj)
    {
        throw new System.NotImplementedException();
    }

    private void OnEnable()
    {
        moveAction.Enable();
        sprintAction.Enable();
        lookAction.Enable();
        aimAction.Enable();
        shootAction.Enable();
        dashAction.Enable();
        pickAction.Enable();
        settingsAction.Enable();
        restartAction.Enable();
    }
    private void OnDisable()
    {
        moveAction.Disable();
        sprintAction.Disable();
        lookAction.Disable();
        aimAction.Disable();
        shootAction.Disable();
        dashAction.Disable();
        pickAction.Disable();
        settingsAction.Disable();
        restartAction.Disable();
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
        lookAction = playerControls.FindActionMap(actionMapName).FindAction(look);
        aimAction = playerControls.FindActionMap(actionMapName).FindAction(aim);
        shootAction = playerControls.FindActionMap(actionMapName).FindAction(shoot);
        dashAction = playerControls.FindActionMap(actionMapName).FindAction(dash);
        pickAction = playerControls.FindActionMap(actionMapName).FindAction(pick);
        settingsAction = playerControls.FindActionMap(actionMapName).FindAction(settings);
        restartAction = playerControls.FindActionMap(actionMapName).FindAction(restart);
        RegisterInputActions();

    }


}
