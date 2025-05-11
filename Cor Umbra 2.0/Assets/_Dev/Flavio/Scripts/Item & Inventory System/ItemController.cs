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
        if(Item.Id == "#001" )
        {
            interactant.GetComponent<PlayerWeaponSelector>().ActiveWeapon.AddAmmo();
        }
        Destroy(gameObject);
    }

    public void Start()
    {
        gameManager = GameManager.instance;
    }

}
