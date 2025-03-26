using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardDevice_Manager : MonoBehaviour, IInteractable
{
    public bool isDoorOpen;
    public Collider doorCollider;
    public Item key;
    public DialogueTrigger dialoguePorta;
    public GameObject keyItem;
    
    public Material[] isMaterial;

    private Material[] renderMaterials;
    private Renderer rendererTerminal;

    private void Awake()
    {
        rendererTerminal = GetComponent<Renderer>();
        renderMaterials = GetComponent<Renderer>().materials;
        if (isDoorOpen)
        {
            OpenedDoor();
        }
        else
        {
            renderMaterials[1] = isMaterial[0];
            rendererTerminal.materials = renderMaterials;
        }
    }
    public void Interact(GameObject interactant)
    {
        if(key != null)
        Debug.Log("Interagiu");
        foreach(Item item in interactant.GetComponent<PlayerInventory>().items)
        {
            if(item != null)
            if(item.Id == key.Id)
            {
                OpenedDoor();
            }
            else if(item == interactant.GetComponent<PlayerInventory>().items.Last<Item>())
            {
                ClosedDoor();
                Debug.Log("Não Possui o Cartão");
            }
        }
        
    }
    private void OpenedDoor()
    {
        keyItem.SetActive(true);
        doorCollider.enabled = true;
        renderMaterials[1] = isMaterial[1];
        rendererTerminal.materials = renderMaterials;
        gameObject.GetComponent<Collider>().enabled = false;
    }
    private void ClosedDoor() 
    {
        dialoguePorta.TriggerDialogue();
    }

}
