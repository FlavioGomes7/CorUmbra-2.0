using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class MultiSliderController : MonoBehaviour
{
    public Volume postProcessVolume; // Referência ao volume de pós-processamento do URP
    private List<Slider> brightnessSliders = new List<Slider>();
    private Dictionary<string, float> minVisibleValues = new Dictionary<string, float>
    {
        { "Slider-brilho", .1f },
        { "Slider-sensibilidade", .1f },
        { "Slider-principalSound", .1f },
        { "Slider-soundFX", .1f }
    };

    void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();

        // Adicione sliders com seus IDs correspondentes
        var sliders = new string[] { "Slider-brilho", "Slider-sensibilidade", "Slider-principalSound", "Slider-soundFX" };

        foreach (var sliderId in sliders)
        {
            var sliderElements = uiDocument.rootVisualElement.Query<Slider>(sliderId).ToList();
            foreach (var slider in sliderElements)
            {
                if (slider != null)
                {
                    float minVisibleValue = minVisibleValues[sliderId];
                    slider.RegisterCallback<PointerDownEvent>(evt =>
                    {
                        UpdateSliderValue(slider, evt, minVisibleValue);
                    });

                    slider.RegisterValueChangedCallback(evt =>
                    {
                        UpdateSliderTracker(slider, evt.newValue, minVisibleValue);
                        ApplySliderSettings(sliderId, evt.newValue);
                    });

                    // Carrega o valor salvo ou define o valor inicial
                    float savedValue = PlayerPrefs.GetFloat(sliderId, minVisibleValue);
                    slider.value = savedValue;

                    // Atualiza o tracker com o valor salvo
                    UpdateSliderTracker(slider, savedValue, minVisibleValue);

                    // Adiciona sliders de brilho à lista para sincronização
                    if (sliderId == "Slider-brilho")
                    {
                        brightnessSliders.Add(slider);
                    }

                    ApplySliderSettings(sliderId, savedValue); // Aplica a configuração inicial
                }
            }
        }
    }

    void UpdateSliderTracker(Slider slider, float value, float minVisibleValue)
    {
        var minValue = slider.lowValue; // Valor mínimo do slider (1)
        var maxValue = slider.highValue; // Valor máximo do slider (4)

        // Calcula a porcentagem correspondente ao valor atual
        float percentage = (value - minValue) / (maxValue - minValue) * 100;

        // Navegar na hierarquia para encontrar o elemento de tracker
        var trackerElement = slider.Q("unity-tracker");
        if (trackerElement != null)
        {
            trackerElement.style.width = new Length(percentage, LengthUnit.Percent);
        }
    }

    void UpdateSliderValue(Slider slider, PointerDownEvent evt, float minVisibleValue)
    {
        var localPosition = evt.localPosition.x;
        var sliderWidth = slider.resolvedStyle.width;

        var minValue = slider.lowValue; // Valor mínimo do slider (1)
        var maxValue = slider.highValue; // Valor máximo do slider (4)

        // Calcula o novo valor baseado na posição do clique
        float newValue = Mathf.Clamp(localPosition / sliderWidth * (maxValue - minValue) + minValue, minVisibleValue, maxValue);
        slider.value = newValue > minVisibleValue ? newValue : minVisibleValue; // Garante que o valor nunca seja menor que 1.1

        UpdateSliderTracker(slider, newValue, minVisibleValue);
        ApplySliderSettings(slider.name, newValue); // Aplica a configuração sempre que o valor é atualizado
    }

    void ApplySliderSettings(string sliderId, float value)
    {
        switch (sliderId)
        {
            case "Slider-brilho":
                SetBrightness(value);
                // Sincroniza todos os sliders de brilho
                foreach (var slider in brightnessSliders)
                {
                    if (slider.value != value)
                    {
                        slider.SetValueWithoutNotify(value);
                        UpdateSliderTracker(slider, value, minVisibleValues["Slider-brilho"]);
                    }
                }
                break;
            case "Slider-sensibilidade":
                SetMouseSensitivity(value);
                break;
            case "Slider-principalSound":
                SetMainVolume(value);
                break;
            case "Slider-soundFX":
                SetSoundFXVolume(value);
                break;
        }

        // Salva o valor do slider
        PlayerPrefs.SetFloat(sliderId, value);
        PlayerPrefs.Save();
    }

    void SetBrightness(float value)
    {
        if (postProcessVolume != null && postProcessVolume.profile.TryGet(out ColorAdjustments colorAdjustments))
        {
            colorAdjustments.postExposure.value = value;
        }
    }

    void SetMouseSensitivity(float value)
    {
        // Supondo que você tenha um gerenciador de entrada para ajustar a sensibilidade do mouse
        // InputManager.Instance.SetMouseSensitivity(value);
    }

    void SetMainVolume(float value)
    {
        // Supondo que você tenha um gerenciador de áudio para ajustar o volume principal
        // AudioManager.Instance.SetMainVolume(value);
    }

    void SetSoundFXVolume(float value)
    {
        // Supondo que você tenha um gerenciador de áudio para ajustar o volume dos efeitos sonoros
        // AudioManager.Instance.SetSoundFXVolume(value);
    }
}
