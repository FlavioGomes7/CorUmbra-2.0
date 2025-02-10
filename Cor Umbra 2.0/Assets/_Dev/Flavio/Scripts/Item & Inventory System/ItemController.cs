using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour, IInteractable
{
    public Item Item;
    private GameManager gameManager;

    public void Interact(GameObject interactant)
    {
        gameManager.AddItem(Item);
        Destroy(gameObject);
    }

    public void Start()
    {
        gameManager = GameManager.instance;
    }

}
