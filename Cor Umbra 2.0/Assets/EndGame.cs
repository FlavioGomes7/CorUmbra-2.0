using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour, IInteractable
{
    public CameraSwitch cameracutscene;
    public void Interact(GameObject interactant)
    {
        cameracutscene.activeCutscene();
        gameObject.SetActive(false);
    }

    
}
