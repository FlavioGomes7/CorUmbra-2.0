using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAirState : State
{
    [SerializeField] private Transform player;
    [SerializeField] private CharacterController chController;
    [SerializeField] private float gravityforce;
    private Vector3 velocity;

    public override void Enter()
    {
        isCompleted = true;
    }
    public override void Do()
    {
        velocity.y += gravityforce * Time.deltaTime;
        chController.Move(velocity * Time.deltaTime);
    }
    public override void Exit()
    {
        velocity.y = 0f;
    }
}
