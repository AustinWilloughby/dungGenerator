using UnityEngine;
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

    private void Flicker() //Changes light intensity and spotAngle based on perlin noise and boss distance to resemble a torch
    {
        float noise = Mathf.PerlinNoise(10, Time.time / 4);

        light.intensity = Mathf.Lerp(1.1f, 1.3f, noise);
        light.spotAngle = Mathf.Lerp(15f, 19f, noise);
        if (distanceToBoss < 15f && distanceToBoss > 6f)
        {
            light.spotAngle = light.spotAngle / (15 / distanceToBoss);
        }
        else if(distanceToBoss < 6f)
        {
            light.spotAngle = light.spotAngle / 2.5f;
        }
        if (distanceToBoss < 10f && distanceToBoss > 6f)
        {
            light.intensity = light.intensity / (10 / distanceToBoss);
        }
        else if (distanceToBoss < 6f)
        {
            light.intensity = light.intensity / (10f / 6f);
        }
    }
}
