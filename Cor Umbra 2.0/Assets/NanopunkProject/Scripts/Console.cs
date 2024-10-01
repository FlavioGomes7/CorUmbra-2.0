using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console : MonoBehaviour
{
    public event Action OnInteraction;
    
    public void InterectConsole()
    {
        OnInteraction?.Invoke();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            InterectConsole();
        }
    }
}
