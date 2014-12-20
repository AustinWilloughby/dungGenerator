using UnityEngine;
using System.Collections;

public class EnemyScript : Vehicle
{
    public float viewDistance = 10f;
    private GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //If the player is nearby
        if (Vector2.Distance(transform.position, player.transform.position) < viewDistance)
        {
            transform.position += (Vector3)Arrive(player.transform.position, 1.5f);
        }
        //Otherwise
        else
        {
            transform.position += (Vector3)Wander();
        }
    }
}
