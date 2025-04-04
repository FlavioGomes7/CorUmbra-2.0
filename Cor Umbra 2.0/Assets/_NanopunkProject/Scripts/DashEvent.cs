using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class DashEvent : MonoBehaviour
{
    private InputHandler inputHandler;
    [SerializeField] private CharacterController chController;
    [SerializeField] private float EvadeTime;

    // Start is called before the first frame update
    void Start()
    {
        inputHandler = InputHandler.instance;
    }

    public void HandleEvade()
    {
        
        StartCoroutine(Evade(chController));
        StartCoroutine(inputHandler.Delay(0.2f, "Evade"));
    }

    public IEnumerator Evade(CharacterController chController)
    {

        Vector3 evadeDirection = new Vector3(inputHandler.moveInput.x, 0, inputHandler.moveInput.y).normalized;
        evadeDirection = evadeDirection.z * transform.forward + evadeDirection.x * transform.right;
        float startTime = Time.time;

        while (Time.time < startTime + EvadeTime)
        {
            chController.Move(evadeDirection * Time.deltaTime * 10f);
            yield return null;
        }
    }
}
