using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightFlicker : MonoBehaviour {
    private Light2D light;

    [SerializeField] private float intensityMin;
    [SerializeField] private float intensityMax;
    [SerializeField] private float intensityPeriod;
    
    [SerializeField] private float rangeMin;
    [SerializeField] private float rangeMax;
    [SerializeField] private float rangePeriod;
    
    private void Awake() {
        light = GetComponent<Light2D>();
    }

    void Update() {
        light.pointLightOuterRadius = rangeMin + (rangeMax - rangeMin) * Mathf.PerlinNoise(Time.time * rangePeriod, 0);
        light.intensity = intensityMin + (intensityMax - intensityMin) * Mathf.PerlinNoise(0, Time.time * intensityPeriod);
    }
}
