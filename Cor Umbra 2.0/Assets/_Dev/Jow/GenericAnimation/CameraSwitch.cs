using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public CinemachineFreeLook thirdPersonCamera; // Câmera de terceira pessoa
    public CinemachineVirtualCamera cutsceneCamera;    // Câmera de cutscene
    public Animator animator;
   
    public Animator doorValve2;
    public void activeCutscene()
    {
        

        cutsceneCamera.Priority = 10;
        animator.Play("ActiveCutscenecamera");

    }
    public void CutsceneOff()
    {
 
        cutsceneCamera.Priority = -1;
    }
    public void DoorOpenValve()
    {
        
        doorValve2.SetBool("DoorOpen", true);
    }


}
