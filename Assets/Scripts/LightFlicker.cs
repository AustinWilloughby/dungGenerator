﻿using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Flicker();
    }

    private void Flicker() //Changes light intensity and spotAngle based on perlin noise and boss distance to resemble a torch
    {
        float noise = Mathf.PerlinNoise(10, Time.time / 4);

        GetComponent<Light>().intensity = Mathf.Lerp(1.6f, 1.9f, noise);
        GetComponent<Light>().spotAngle = Mathf.Lerp(15f, 19f, noise);
    }
}
