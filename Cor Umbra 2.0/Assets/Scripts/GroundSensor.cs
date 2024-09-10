using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    public bool grounded { get; private set; }
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundedDistance;
    [SerializeField] private LayerMask groundMask;
    private void FixedUpdate()
    {
        CheckGround();
    }

    public void CheckGround()
    {
        grounded = Physics.CheckSphere(groundCheck.position, groundedDistance, groundMask); 
    }
}
