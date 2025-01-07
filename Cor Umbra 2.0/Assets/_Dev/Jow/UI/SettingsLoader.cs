using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class SettingsLoader : MonoBehaviour
{
    [SerializeField] private Volume postProcessVolume;
    [SerializeField] private float brightness;
    private void Awake()
    {
        brightness = PlayerPrefs.GetFloat("Slider-brilho");
    }
    void Start()
    {
        ApplyBrightness(brightness);
    }
    void ApplyBrightness(float value)
    {
           if (postProcessVolume != null && postProcessVolume.profile.TryGet(out ColorAdjustments colorAdjustments))
            {
                colorAdjustments.postExposure.value = value;
            }
    }
}
