using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyScript : Vehicle
{
    public float viewDistance = 10f;
    private GameObject player;
    private StatTracker stats;
    public SpawnerHandler spawner;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stats = this.GetComponent<StatTracker>();
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

        //If their health drops to 0 or below, kill them
        if (stats.health <= 0)
        {
            spawner.KillSpawn();
            GameObject.Destroy(gameObject);
        }
    }
}
