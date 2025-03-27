using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveSystem : MonoBehaviour, IInteractable
{
    [SerializeField] private Collider[] colliderslocks;
    [SerializeField] private Collider[] otherColliders;

    public void Interact(GameObject interactant)
    {
        Debug.Log("ativou");
        if (colliderslocks != null)
        {
            foreach(Collider collider in colliderslocks)
            {
                collider.enabled = false;
            }

            foreach(Collider collider in otherColliders)
            {
                collider.enabled=true;
            }
        }

        
    }
}
