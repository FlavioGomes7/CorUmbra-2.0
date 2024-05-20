using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController chController;
    private InputHandler inputHandler;
    [SerializeField]private CinemachineFreeLook freeLook;
    private Vector3 playerDirection;
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float sprintMultiplier;


    public void HandleMovement()
    {
        float speed = playerSpeed * (inputHandler.sprintValue > 0 ? sprintMultiplier : 1f);
        playerDirection = new Vector3(inputHandler.moveInput.x, 0f, inputHandler.moveInput.y);
        playerDirection = playerDirection.x * transform.right + playerDirection.z * transform.forward;
        if (playerDirection.magnitude > 0)
        {
            chController.Move(playerDirection * Time.deltaTime * speed);
            transform.eulerAngles = new Vector3(transform.localEulerAngles.x, freeLook.m_XAxis.Value, transform.localEulerAngles.z);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        chController = GetComponent<CharacterController>();
        inputHandler = InputHandler.instance;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();     
    }


}
