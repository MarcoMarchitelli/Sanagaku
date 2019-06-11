using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class PulsingLight : MonoBehaviour
{
    public float maxIntensity = 1f;
    public float minIntensity = 0f;
    public float pulseSpeed = 1f; //here, a value of 0.5f would take 2 seconds and a value of 2f would take half a second

    new private Light light;
    private float targetIntensity = 1f;
    private float currentIntensity;

    void Start()
    {
        light = GetComponent<Light>();
    }

    void Update()
    {
        currentIntensity = Mathf.MoveTowards(light.intensity, targetIntensity, Time.deltaTime * pulseSpeed);
        if (currentIntensity >= maxIntensity)
        {
            currentIntensity = maxIntensity;
            targetIntensity = minIntensity;
        }
        else if (currentIntensity <= minIntensity)
        {
            currentIntensity = minIntensity;
            targetIntensity = maxIntensity;
        }
        light.intensity = currentIntensity;
    }
}