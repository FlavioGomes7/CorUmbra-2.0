using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlickeringEmissive : MonoBehaviour
{
    public Material emissive;
    public Color EmissiveDColor;
    public float EmMultiplicador;
    public Light myLight;
    public float maxInterval = 1f;

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
        EmMultiplicador = 1;
        EmissiveDColor = emissive.GetColor("_EmissionColor");
        myLight = GetComponent<Light>();
        origin = transform.position;
        lastPosition = origin;
    }

    void Update()
    {
        emissive.SetColor("_EmissionColor", EmissiveDColor * EmMultiplicador);
    }
}