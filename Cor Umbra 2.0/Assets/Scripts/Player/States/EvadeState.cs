using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class EvadeState : State
{
    [SerializeField] private CharacterController chController;
    [SerializeField] private float evadeTime;
    private InputHandler inputHandler;
    public override void Enter()
    {
        inputHandler = InputHandler.instance;
        StartCoroutine(Evade());
        StartCoroutine(inputHandler.Delay(0.2f, "Evade"));
    }
    public override void Do()
    {
        
    }

    public IEnumerator Evade()
    {

        Vector3 evadeDirection = new Vector3(inputHandler.moveInput.x, 0, inputHandler.moveInput.y).normalized;
       
        evadeDirection = evadeDirection.z * transform.forward + evadeDirection.x * transform.right;
        if(evadeDirection == Vector3.zero)
        {
            evadeDirection = -transform.forward * 0.5f;
        }
        float startTime = Time.time;

        while (Time.time < startTime + evadeTime)
        {
            chController.Move(evadeDirection * Time.deltaTime * 5f);
            yield return null;
        }
        isCompleted = true;
    }
}
