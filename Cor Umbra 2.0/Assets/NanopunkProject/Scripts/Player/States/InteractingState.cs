using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractingState : State
{
    [SerializeField] private LayerMask interactableMask = new LayerMask();

    public override void Enter()
    {
       
    }

    public override void Do()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 2f, interactableMask))
        {
           if(raycastHit.transform.gameObject.GetComponent<IInteractable>() != null)
            {
                raycastHit.transform.gameObject.GetComponent<IInteractable>().Interact(this.gameObject);
            }
        }
    }
}
