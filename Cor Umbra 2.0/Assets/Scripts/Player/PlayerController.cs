using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController chController;
    private InputHandler inputHandler;
    [SerializeField]private CinemachineFreeLook freeLook;
    [SerializeField] private CinemachineVirtualCamera aimCamera;
    [SerializeField] private GameObject followTarget;
    [SerializeField] private GameObject crosshair;
    private Vector3 playerDirection;
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float sensitivy;
    [SerializeField] private float sprintMultiplier;
    [SerializeField] private float evadeTime;
    [SerializeField] private float turnSmoothTime;
    [SerializeField] private LayerMask aimColliderMask = new LayerMask();
    [SerializeField] private Transform debugTransform;
    private float turnSmoothVelocity;
    private bool isAim;
    private bool isEvade;


    public void HandleMovement()
    {
        float speed = playerSpeed * (inputHandler.sprintValue > 0 && inputHandler.moveInput.y > -0.5f ? sprintMultiplier : 1f);
        playerDirection = new Vector3(inputHandler.moveInput.x, 0f, inputHandler.moveInput.y).normalized;
        playerDirection = playerDirection.x * transform.right + playerDirection.z * transform.forward;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, freeLook.m_XAxis.Value, ref turnSmoothVelocity, turnSmoothTime);
        if (playerDirection.magnitude > 0 && !isAim && !isEvade)
        {
            chController.Move(playerDirection * Time.deltaTime * speed);
            //transform.localRotation = Quaternion.Euler(0, followTarget.transform.rotation.y, 0);
            //transform.rotation = Quaternion.Euler(0, followTarget.transform.rotation.eulerAngles.y, 0);
            transform.eulerAngles = new Vector3(transform.localEulerAngles.x, angle, transform.localEulerAngles.z);
        }

    }

    //void HandleCamera()
    //{
    //    followTarget.transform.rotation *= Quaternion.AngleAxis(inputHandler.lookValue.x * sensitivy, Vector3.up);
    //    followTarget.transform.rotation *= Quaternion.AngleAxis(inputHandler.lookValue.y * sensitivy, Vector3.right);

    //    var angles = followTarget.transform.eulerAngles;
    //    angles.z = 0f;

    //    if (angles.x > 180f && angles.x < 340f)
    //    {
    //        angles.x = 340f;
    //    }
    //    else if (angles.x < 180f && angles.x > 40)
    //    {
    //        angles.x = 40f;
    //    }

    //    followTarget.transform.localEulerAngles = angles;
    //    //followTarget.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
    //}

    public void HandleEvade()
    {
        if(inputHandler.dashTriggered) 
        {
            isEvade = true;
            StartCoroutine(Evade());
            StartCoroutine(inputHandler.Delay(0.2f, "Evade"));
        }
    }

    public IEnumerator Evade()
    {
        
        Vector3 evadeDirection = new Vector3(inputHandler.moveInput.x, 0, inputHandler.moveInput.y).normalized;
        evadeDirection = evadeDirection.z * transform.forward + evadeDirection.x * transform.right;
        float startTime = Time.time;

        while(Time.time < startTime + evadeTime)
        {
            chController.Move(evadeDirection * Time.deltaTime * 10f);
            yield return null;
        }
        isEvade = false;
    }

    public void HandleAim()
    {
        if(inputHandler.aimTriggered)
        {
            Vector3 mouseWorldPosition = Vector3.zero;
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

            isAim = true;
            crosshair.SetActive(true);
            freeLook.Priority = 0;
            aimCamera.Priority = 1;

            playerDirection = new Vector3(inputHandler.moveInput.x, 0f, inputHandler.moveInput.y).normalized;
            playerDirection = playerDirection.x * transform.right + playerDirection.z * transform.forward;

            chController.Move(playerDirection * playerSpeed * Time.deltaTime);

            followTarget.transform.rotation *= Quaternion.AngleAxis(inputHandler.lookValue.x * sensitivy, Vector3.up);
            followTarget.transform.rotation *= Quaternion.AngleAxis(-inputHandler.lookValue.y * sensitivy, Vector3.right);

            var angles = followTarget.transform.eulerAngles;
            angles.z = 0;
          
            followTarget.transform.localEulerAngles = angles;
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderMask))
            {
                debugTransform.position = raycastHit.point;
                mouseWorldPosition = raycastHit.point;
            }

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            isAim = false;
            crosshair.SetActive(false);
            freeLook.Priority = 1;
            aimCamera.Priority = 0;
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
        followTarget.transform.position = new Vector3(transform.position.x, followTarget.transform.position.y, transform.position.z);
        HandleEvade();
        HandleAim();
        HandleMovement();
        //HandleCamera();      
    }


}
