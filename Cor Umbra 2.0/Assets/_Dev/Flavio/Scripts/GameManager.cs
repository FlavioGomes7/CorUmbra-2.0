using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set;}

    private bool active = false;

    private InputHandler inputHandler;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private AimingState aimingState;
    [SerializeField] private AnimationStateController animationStateController;
    [SerializeField] private CinemachineFreeLook freeLook;
    [SerializeField] private GameObject PanelConfig;

    public void AddItem(Item item)
    {
        playerInventory.items.Add(item);
    }
    public void RemoveItem(Item item) 
    {
        playerInventory.items.Remove(item);
    }

    public void OnSensitivyChange(float value)
    {
        aimingState.sensitivy = value;
    }

    public void DisableEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(var enemy in enemies)
        {
            enemy.GetComponent<HumanoidEnemy>().enabled = false;
        }
    }

    private void OpenConfig()
    {
        if(active == false)
        {
            playerController.enabled = false;
            animationStateController.enabled = false;
            freeLook.m_XAxis.m_MaxSpeed = 0;
            freeLook.m_YAxis.m_MaxSpeed = 0;
            PanelConfig.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            active = true;
        }
        else if(active == true)
        {
            playerController.enabled = true;
            animationStateController.enabled = true;
            freeLook.m_XAxis.m_MaxSpeed = 100f;
            freeLook.m_YAxis.m_MaxSpeed = 1f;
            PanelConfig.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            active = false;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

    }

    void Start()
    {
        inputHandler = InputHandler.instance;
    }

    
    void Update()
    {
        if (inputHandler.settingsTriggered)
        {
            inputHandler.Delay(1f, "Settings");
            OpenConfig();
        }
    }
}
