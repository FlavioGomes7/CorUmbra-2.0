using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class HealingState : State
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerInventory inventory;

    private float healingAmount = 40f;
    public int medkitAmount;


    public override void Enter()
    {
        
        if (playerController.CurrentHealth + healingAmount <= playerController.MaxHealth && medkitAmount > 0)
        {
            playerController.Healing(healingAmount, false);
            medkitAmount--;
            
        }
        else if(playerController.CurrentHealth + healingAmount > playerController.MaxHealth && medkitAmount > 0)
        {
            playerController.Healing(healingAmount, true);
            medkitAmount--;   
        }
    
    }

    public override void Do()
    {
        if(time > 0.5f)
        {
            isCompleted = true;
        }
    }

    public void AddMedkit()
    {
        foreach (Item item in inventory.items)
        {
            if (item != null)
            {
                if (item.Id == "#002")
                {
                    medkitAmount += item.Amount;
                    inventory.items.Remove(item);
                    break;
                }
            }

        }
    }

}
