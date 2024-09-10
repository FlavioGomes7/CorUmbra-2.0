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


    [SerializeField] private Collider pickingArea;

    private void HandlePickItem()
    {
        if(inputHandler.pickTriggered)
        {
            pickingArea.enabled = true;
        }
        else if(!inputHandler.pickTriggered)
        {
            pickingArea.enabled = false;
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
        HandlePickItem();
    }


}
