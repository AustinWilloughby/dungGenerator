﻿using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour
{
    //Fields
    //Private
    private GameObject player;
    private GameObject boss;
    private float distanceToBoss;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        boss = GameObject.Find("Boss");
        distanceToBoss = Vector2.Distance((Vector2)player.transform.position, (Vector2)boss.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        distanceToBoss = Vector2.Distance((Vector2)player.transform.position, (Vector2)boss.transform.position);
        Flicker();
    }

    private void Flicker()
    {
        float noise = Mathf.PerlinNoise(10, Time.time / 4);

        light.intensity = Mathf.Lerp(.9f, 1f, noise);
        light.spotAngle = Mathf.Lerp(13f, 17f, noise);
        if (distanceToBoss < 15f)
        {
            light.spotAngle = light.spotAngle / (15 / distanceToBoss) + 1;
            light.intensity = light.intensity / (15 / distanceToBoss) + 1;
        }
    }


}
