using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    public bool grounded { get; private set; }
    private RaycastHit hit;
    private float bufferCheckDistance = 0.1f; //Um pouco acima do zero 
    private float groundedCheckDistance;
    [SerializeField] private CharacterController chController;
    [SerializeField] private LayerMask groundMask;
    private void FixedUpdate()
    {
        CheckGround();
    }

    public void CheckGround()
    {
        groundedCheckDistance = (chController.height / 25) + bufferCheckDistance;
        if(Physics.Raycast(transform.position, -transform.up, out hit, groundedCheckDistance, groundMask))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        Debug.DrawRay(transform.position, transform.up, Color.red);    
    }
}
