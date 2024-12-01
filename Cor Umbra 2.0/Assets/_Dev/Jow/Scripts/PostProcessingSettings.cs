using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;

public class PostProcessingSettings : MonoBehaviour
{
    public Volume postProcessingVolume;
    public GameObject settingsPanel;
    public GameObject player; // Referência ao objeto do jogador ou script de controle

    [Header("Sliders")]
    //public Slider bloomSlider;
    public Slider exposureSlider;

    private Bloom bloom;
    private ColorAdjustments colorAdjustments;

    // Crie uma referência ao mapa de ação de entrada
    private InputAction toggleAction;

    void Awake()
    {
        // Inicialize o mapa de ação de entrada
        toggleAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/p");
        toggleAction.performed += ctx => ToggleSettingsPanel();
        toggleAction.Enable();
    }

    void Start()
    {
        postProcessingVolume.profile.TryGet(out bloom);
        postProcessingVolume.profile.TryGet(out colorAdjustments);

        //bloomSlider.onValueChanged.AddListener(SetBloom);
        exposureSlider.onValueChanged.AddListener(SetExposure);

        settingsPanel.SetActive(false); // Oculta o painel ao iniciar
        ToggleCursor(false); // Inicia com o cursor bloqueado
    }

    void OnDestroy()
    {
        // Desabilite a ação quando o objeto for destruído
        toggleAction.Disable();
    }

    void ToggleSettingsPanel()
    {
        bool isActive = !settingsPanel.activeSelf;
        settingsPanel.SetActive(isActive); // Alterna a visibilidade do painel
        player.SetActive(!isActive); // Desabilita o jogador quando o menu está ativo
        ToggleCursor(isActive); // Alterna o estado do cursor
    }

    void ToggleCursor(bool isVisible)
    {
        if (isVisible)
        {
            Cursor.lockState = CursorLockMode.None; // Libera o cursor
            Cursor.visible = true; // Torna o cursor visível
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; // Trava o cursor
            Cursor.visible = false; // Torna o cursor invisível
        }
    }

    void SetBloom(float value)
    {
        if (bloom != null)
        {
            bloom.intensity.value = value;
        }
    }

    void SetExposure(float value)
    {
        if (colorAdjustments != null)
        {
            colorAdjustments.postExposure.value = value;
        }
    }
}
