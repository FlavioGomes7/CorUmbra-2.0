using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    private bool HasKeyItem = false;
    [SerializeField] private KeyItem KeyItem;
    [SerializeField] private Console Console;
    [SerializeField] private Material MaterialConsole;
    [SerializeField] private GameObject Wall;

    private void OnTakeKeyItem()
    {
        HasKeyItem = true;
        Debug.Log("Pegou");
    }

    private void OnInteractionConsole()
    {
        if (!HasKeyItem)
        {
            MaterialConsole.color = Color.red;
        }
        else
        {
            MaterialConsole.color = Color.green;
            Wall.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if (KeyItem != null)
        {
            KeyItem.OnInteraction += OnTakeKeyItem;
        }

        if (Console != null)
        {
            Console.OnInteraction += OnInteractionConsole;
        }
    }

    private void OnDisable()
    {
        if (KeyItem != null)
        {
            KeyItem.OnInteraction -= OnTakeKeyItem;
        }

        if (Console != null)
        {
            Console.OnInteraction -= OnInteractionConsole;
        }
  
    }


}
