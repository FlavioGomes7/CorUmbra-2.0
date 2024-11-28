using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Core
{
    //Estados
    [SerializeField] private StandardState standardState;
    [SerializeField] private OnAirState onAirState;
    [SerializeField] private HittedState hittedState;
    [SerializeField] private InteractingState interactingState;

    private CharacterController chController;
    private InputHandler inputHandler;

    private bool interactbleInRange = false;
    [SerializeField] private Collider pickingArea;
    [SerializeField] private LayerMask interactableMask = new LayerMask();

    private void HandleInteractable()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        if(Physics.Raycast(ray, out RaycastHit raycastHit, 2f, interactableMask))
        {
            interactbleInRange = true;
        }
        else
        {
            interactbleInRange = false;
        }

    }
    private void SelectState()
    {
        if(state.isCompleted)
        {
            if (groundSensor.grounded)
            {
                Set(standardState);
            }
            else
            {
                Set(onAirState);
            }
        }
        if(inputHandler.interactTriggered && groundSensor.grounded && interactbleInRange)
        {
            Set(interactingState);
        }
        state.DoBranch();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetupInstances();
        Set(standardState);
        chController = GetComponent<CharacterController>();
        inputHandler = InputHandler.instance;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        SelectState();
        HandleInteractable();
    }


}
