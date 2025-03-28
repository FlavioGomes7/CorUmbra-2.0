using Cinemachine;
using System;
using TMPro;
using UnityEngine;

public class PlayerController : Core, IDamageable
{
    //Stats
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    

    //Estados
    [SerializeField] private StandardState standardState;
    [SerializeField] private OnAirState onAirState;
    [SerializeField] private HittedState hittedState;
    [SerializeField] private InteractingState interactingState;

    private CharacterController chController;
    private InputHandler inputHandler;

    private bool interactbleInRange = false;
    public RaycastHit interactableHit;
    public Collider Hitbox;
    [SerializeField] private LayerMask interactableMask = new LayerMask();
    [SerializeField] private HealthBar healthBar;
    public PlayerWeaponSelector weaponSelector;
    public TextMeshProUGUI AmmoText;

    public static event IDamageable.TakeDamageEvent OnTakeDamage;
    public event IDamageable.DeathEvent OnDeath;

    public float CurrentHealth { get => currentHealth; private set => currentHealth = value; }

    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }


    public void TakeDamage(float damage, Collider collider, Vector3 hitPoint)
    {
        float damageTaken = Mathf.Clamp(damage, 0, currentHealth);
        CurrentHealth -= damageTaken;
        healthBar.RemoveHealth(damageTaken);
        if(state.isCompleted)
        {
            Set(hittedState, true);
        }

    }


    private void HandleInteractable()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, 2f, interactableMask))
        {
            interactableHit = raycastHit;
            interactbleInRange = true;
        }
        else
        {
            interactbleInRange = false;
        }

    }
    private void SelectState()
    {
        if(state.isCompleted && currentHealth > 0)
        {
            if (groundSensor.grounded)
            {
                Set(standardState);
            }
            else
            {
                Set(onAirState);
            }
        }
        if (inputHandler.interactTriggered && groundSensor.grounded && interactbleInRange && state.isCompleted)
        {
            Set(interactingState);
        }
        state.DoBranch();
    }
    public void UpdateTextAmmo()
    {
        AmmoText.text =weaponSelector.ActiveWeapon.weaponAmmo.ToString("00");
    }

    // Start is called before the first frame update
    void Start()
    {
        SetupInstances();
        Set(standardState);
        currentHealth = maxHealth;
        UpdateTextAmmo();
        chController = GetComponent<CharacterController>();
        inputHandler = InputHandler.instance;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentHealth <= 0)
        {
            healthBar.gameObject.SetActive(false);
            inputHandler.Disable();
        }
        SelectState();
        HandleInteractable();
    }

    
}
