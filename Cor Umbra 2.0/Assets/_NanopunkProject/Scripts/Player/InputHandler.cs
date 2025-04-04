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
    [SerializeField] private string reload = "Reload";
    [SerializeField] private string dash = "Dash";
    [SerializeField] private string interact = "Interact";
    [SerializeField] private string settings = "Settings";

    private InputAction moveAction;
    private InputAction sprintAction;
    private InputAction lookAction;
    private InputAction aimAction;
    private InputAction shootAction;
    private InputAction reloadAction;
    private InputAction dashAction;
    private InputAction interactAction;
    private InputAction settingsAction;

    public Vector2 moveInput { get; private set; }
    public Vector2 lookValue { get; private set;}
    public float sprintValue { get; private set; }
    public bool aimTriggered { get; private set; }
    public bool shootTriggered { get; private set; }
    public bool reloadTriggered { get; private set; }
    public bool dashTriggered { get; private set; }
    public bool interactTriggered { get; private set; }
    public bool settingsTriggered { get; private set; }

    public static InputHandler instance { get; private set; }


    private void RegisterInputActions()
    {
        moveAction.performed += context => moveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => moveInput = Vector2.zero;

        sprintAction.performed += context => sprintValue = context.ReadValue<float>();
        sprintAction.canceled += context => sprintValue = 0f;

        lookAction.performed += context => lookValue = context.ReadValue<Vector2>().normalized;
        lookAction.canceled += context => lookValue = Vector2.zero;

        aimAction.performed += context => aimTriggered = true;
        aimAction.canceled += context => aimTriggered = false;

        shootAction.performed += context => shootTriggered = true;
        shootAction.canceled += context => shootTriggered = false;

        reloadAction.performed += context => reloadTriggered = true;
        reloadAction.canceled += context => reloadTriggered = false;

        dashAction.performed += context => dashTriggered = true;
        dashAction.canceled += context => dashTriggered = false;

        interactAction.performed += context => interactTriggered = true;
        interactAction.canceled += context => interactTriggered = false;

        settingsAction.performed += context => settingsTriggered = true;
        settingsAction.canceled += context => settingsTriggered = false;
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
        reloadAction.Enable();
        dashAction.Enable();
        interactAction.Enable();
        settingsAction.Enable();
    }
    private void OnDisable()
    {
        moveAction.Disable();
        sprintAction.Disable();
        lookAction.Disable();
        aimAction.Disable();
        shootAction.Disable();
        reloadAction.Disable();
        dashAction.Disable();
        interactAction.Disable();
        settingsAction.Disable();
    }

    public IEnumerator Delay(float delay, string action)
    {
        InputAction inputAction = playerControls.FindActionMap(actionMapName).FindAction(action);
        inputAction.Disable();
        yield return new WaitForSeconds(delay);
        inputAction.Enable();
        StopCoroutine(Delay(delay, action));
    }

    public void Disable()
    {
        moveAction.Disable();
        sprintAction.Disable();
        lookAction.Disable();
        aimAction.Disable();
        shootAction.Disable();
        reloadAction.Disable();
        dashAction.Disable();
        interactAction.Disable();
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
        reloadAction = playerControls.FindActionMap(actionMapName).FindAction(reload);
        dashAction = playerControls.FindActionMap(actionMapName).FindAction(dash);
        interactAction = playerControls.FindActionMap(actionMapName).FindAction(interact);
        settingsAction = playerControls.FindActionMap(actionMapName).FindAction(settings);
        RegisterInputActions();

    }


}
