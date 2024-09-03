using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAirState : State
{
    [SerializeField] private Transform player;
    [SerializeField] private CharacterController chController;
    [SerializeField] private float gravityforce;
    private Vector3 playerDirection;

    public override void Enter()
    {
        playerDirection = player.position;
    }
    public override void Do()
    {
        playerDirection.y += gravityforce;
        chController.Move(playerDirection * Time.deltaTime);
        if(groundSensor.grounded)
        {
            isCompleted = true;
        }
    }
    public override void Exit()
    {

    }
}
