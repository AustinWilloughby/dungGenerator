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

    private void Flicker()
    {
        if (distanceToBoss > 15f)
        {
            float noise = Mathf.PerlinNoise(Random.Range(0.0f, 65535.0f), Time.time / 5f);
            light.intensity = Mathf.Lerp(.8f, 1.1f, noise);
        }
        else
        {
            light.spotAngle = distanceToBoss / 1.2f;
        }
    }


}
