using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour, IInteractable
{
    public void Interact(GameObject interactant)
    {
        SceneManager.LoadScene(0);
    }

    
}
