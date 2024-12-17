using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractingState : State
{

    [SerializeField] private PlayerController playerController;

    public override void Enter()
    {
        if (playerController.interactableHit.transform.GetComponent<IInteractable>() != null)
        {
            isCompleted = true;
            playerController.interactableHit.transform.GetComponent<IInteractable>().Interact();
        }
        else
        {
            isCompleted = true;
        }
    }

    public override void Do()
    {
       
    }
}
