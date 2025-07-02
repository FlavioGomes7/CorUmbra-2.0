using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LockedDoor : MonoBehaviour, IInteractable
{
    public Collider doorCollider;
    public Item key;
    public void Interact(GameObject interactant)
    {
        Debug.Log("Interagiu");
        foreach(Item item in interactant.GetComponent<PlayerInventory>().items)
        {
            if(item.Id == key.Id)
            {
                
                doorCollider.enabled = true;
            }
            else if(item == interactant.GetComponent<PlayerInventory>().items.Last<Item>())
            {
                Debug.Log("Não Possui o Cartão");
            }
        }
        
    }

}
