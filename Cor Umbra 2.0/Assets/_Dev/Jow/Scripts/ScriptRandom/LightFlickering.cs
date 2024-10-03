using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightFlickering : MonoBehaviour
{
    public Light myLight;
    public float maxInterval = 1f;

    public Material emissive;
    public Color EmissiveDColor;
    public float EmMultiplicador;
    float lastIntensityE;
    float EmissionRGB;
    float EMintensity;

    float targetIntensity;
    float lastIntensity;
    float interval;
    float timer;
    public float rMinIntesity;
    public float rMaxIntesity;

    public float maxDisplacement = 0.25f;
    Vector3 targetPosition;
    Vector3 lastPosition;
    Vector3 origin;

    private void Start()
    {
        EmissionRGB = (EmissiveDColor.r / 255) * (EmissiveDColor.g / 255) * (EmissiveDColor.b / 255);
        EmMultiplicador = 1;
        emissive.SetColor("_EmissionColor", EmissiveDColor);
        myLight = GetComponent<Light>();
        origin = transform.position;
        lastPosition = origin;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > interval)
        {
            lastIntensity = myLight.intensity;
            lastIntensityE = EmissionRGB * EmMultiplicador  ;
            targetIntensity = Random.Range(rMinIntesity, rMaxIntesity);
            timer = 0;
            interval = Random.Range(0, maxInterval);

            targetPosition = origin + Random.insideUnitSphere * maxDisplacement;
            lastPosition = myLight.transform.position;
        }

        myLight.intensity = Mathf.Lerp(lastIntensity, targetIntensity, timer / interval);
        EMintensity = Mathf.Lerp(lastIntensityE, targetIntensity, timer / interval);
        emissive.SetColor("_EmissionColor", EmissiveDColor * EMintensity);
        myLight.transform.position = Vector3.Lerp(lastPosition, targetPosition, timer / interval);
    }
}