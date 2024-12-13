using UnityEngine;
using UnityEngine.InputSystem;

public class FreeFlyCameraController : MonoBehaviour
{
    [SerializeField]
    private Camera _freeFlyCamera;

    [SerializeField]
    private Camera _mainCamera;

    [SerializeField]
    [Tooltip("The script is currently active")]
    private bool _active = false;

    [Space]
    [SerializeField]
    [Tooltip("Camera rotation by mouse movement is active")]
    private bool _enableRotation = true;

    [SerializeField]
    [Tooltip("Sensitivity of mouse rotation")]
    private float _mouseSense = 1.8f;

    [Space]
    [SerializeField]
    [Tooltip("Camera zooming in/out by 'Mouse Scroll Wheel' is active")]
    private bool _enableTranslation = true;

    [SerializeField]
    [Tooltip("Velocity of camera zooming in/out")]
    private float _translationSpeed = 55f;

    [Space]
    [SerializeField]
    [Tooltip("Camera movement by 'W','A','S','D','Q','E' keys is active")]
    private bool _enableMovement = true;

    [SerializeField]
    [Tooltip("Camera movement speed")]
    private float _movementSpeed = 10f;

    [SerializeField]
    [Tooltip("Speed of the quick camera movement when holding the 'Left Shift' key")]
    private float _boostedSpeed = 50f;

    [SerializeField]
    [Tooltip("Acceleration at camera movement is active")]
    private bool _enableSpeedAcceleration = true;

    [SerializeField]
    [Tooltip("Rate which is applied during camera movement")]
    private float _speedAccelerationFactor = 1.5f;

    [SerializeField]
    private InputAction _toggleFreeFlyAction;

    private Vector2 _moveInput;
    private Vector2 _lookInput;
    private bool _boostInput;
    private float _translateInput;

    private CursorLockMode _wantedMode;
    private float _currentIncrease = 1;
    private float _currentIncreaseMem = 0;
    private Vector3 _initPosition;
    private Vector3 _initRotation;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_boostedSpeed < _movementSpeed)
            _boostedSpeed = _movementSpeed;
    }
#endif

    private void Start()
    {
        if (_freeFlyCamera != null)
        {
            _initPosition = _freeFlyCamera.transform.position;
            _initRotation = _freeFlyCamera.transform.eulerAngles;

            _freeFlyCamera.gameObject.SetActive(_active);
        }

        _toggleFreeFlyAction.Enable();
        _toggleFreeFlyAction.performed += _ => ToggleFreeFlyCamera();
    }

    private void OnEnable()
    {
        var playerInput = new InputActionMap("Player");

        var moveAction = playerInput.AddAction("Move", binding: "<Keyboard>/w|<Keyboard>/a|<Keyboard>/s|<Keyboard>/d");
        moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");
        moveAction.performed += context => _moveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => _moveInput = Vector2.zero;

        var lookAction = playerInput.AddAction("Look", binding: "<Mouse>/delta");
        lookAction.performed += context => _lookInput = context.ReadValue<Vector2>();
        lookAction.canceled += context => _lookInput = Vector2.zero;

        var boostAction = playerInput.AddAction("Boost", binding: "<Keyboard>/leftShift");
        boostAction.performed += context => _boostInput = context.ReadValue<float>() > 0;
        boostAction.canceled += context => _boostInput = false;

        var translateAction = playerInput.AddAction("Translate", binding: "<Mouse>/scroll");
        translateAction.performed += context => _translateInput = context.ReadValue<Vector2>().y;
        translateAction.canceled += context => _translateInput = 0;

        playerInput.Enable();
    }

    private void OnDisable()
    {
        _toggleFreeFlyAction.Disable();
    }

    private void Update()
    {
        if (!_active || _freeFlyCamera == null)
            return;

        SetCursorState();

        if (Cursor.visible)
            return;

        // Translation
        if (_enableTranslation)
        {
            _freeFlyCamera.transform.Translate(Vector3.forward * _translateInput * Time.deltaTime * _translationSpeed);
        }

        // Movement
        if (_enableMovement)
        {
            Vector3 deltaPosition = Vector3.zero;
            float currentSpeed = _movementSpeed;

            if (_boostInput)
                currentSpeed = _boostedSpeed;

            deltaPosition += _freeFlyCamera.transform.forward * _moveInput.y;
            deltaPosition += _freeFlyCamera.transform.right * _moveInput.x;

            // Calc acceleration
            CalculateCurrentIncrease(deltaPosition != Vector3.zero);

            _freeFlyCamera.transform.position += deltaPosition * currentSpeed * _currentIncrease;
        }

        // Rotation
        if (_enableRotation)
        {
            // Pitch
            _freeFlyCamera.transform.rotation *= Quaternion.AngleAxis(-_lookInput.y * _mouseSense, Vector3.right);

            // Yaw
            _freeFlyCamera.transform.rotation = Quaternion.Euler(_freeFlyCamera.transform.eulerAngles.x, _freeFlyCamera.transform.eulerAngles.y + _lookInput.x * _mouseSense, _freeFlyCamera.transform.eulerAngles.z);
        }
    }

    private void SetCursorState()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
            Cursor.lockState = CursorLockMode.None;

        if (Mouse.current.leftButton.wasPressedThisFrame)
            Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = (CursorLockMode.Locked != Cursor.lockState);
    }

    private void CalculateCurrentIncrease(bool moving)
    {
        _currentIncrease = Time.deltaTime;

        if (!_enableSpeedAcceleration || !moving)
        {
            _currentIncreaseMem = 0;
            return;
        }

        _currentIncreaseMem += Time.deltaTime * (_speedAccelerationFactor - 1);
        _currentIncrease = Time.deltaTime + Mathf.Pow(_currentIncreaseMem, 3) * Time.deltaTime;
    }

    private void ToggleFreeFlyCamera()
    {
        _active = !_active;
        _freeFlyCamera.gameObject.SetActive(_active);

        if (_mainCamera != null)
        {
            _mainCamera.gameObject.SetActive(!_active);
        }

        Debug.Log("Toggle Free Fly Camera: " + _active);
        Debug.Log("Main Camera Active: " + _mainCamera.gameObject.activeSelf);
    }
}
