using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyScript : Vehicle
{
    public float viewDistance = 10f;
    private GameObject player;
    private StatTracker stats;
    public SpawnerHandler spawner;
    public LayerMask raycastLayers;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stats = this.GetComponent<StatTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementHandler();
        DeathCheck();
    }

    void MovementHandler() //Handles all movements made by the enemy
    {
        if (Vector2.Distance(transform.position, player.transform.position) < viewDistance)
        {
            //Cast a ray towards the player
            RaycastHit2D sightLine = Physics2D.Raycast(transform.position, player.transform.position - transform.position, viewDistance);

            //If the ray doesnt hit a wall, chase the player
            if (sightLine.collider.gameObject.tag != "Wall")
            {
                transform.position += (Vector3)Arrive(player.transform.position, 1.2f);
            }
            //Otherwise wander
            else
            {
                transform.position += (Vector3)Wander();
            }
        }
        //Otherwise
        else
        {
            transform.position += (Vector3)Wander();
        }
    }

    void DeathCheck() //Checks if the enemy is dead
    {
        //If their health drops to 0 or below, kill them
        if (stats.health <= 0)
        {
            spawner.KillSpawn();
            GameObject.Destroy(gameObject);
        }
    }
}
