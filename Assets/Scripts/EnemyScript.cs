using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyScript : Vehicle
{
    public float viewDistance = 10f;
    public float attackDistance = 2f;
    private GameObject player;
    private StatTracker stats;
    public SpawnerHandler spawner;
    public Vector2 direction;
    private GameObject weapon;
    public float attackTimer = 2f;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stats = this.GetComponent<StatTracker>();
        direction = Vector2.zero;
        weapon = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        direction = Vector2.zero;
        MovementHandler();
        SpriteRotator(direction);
        DeathCheck();
    }

    void MovementHandler() //Handles all movements made by the enemy
    {
        if (Vector2.Distance((Vector2)transform.position, (Vector2)player.transform.position) < viewDistance)
        {
            //Cast a ray towards the player
            RaycastHit2D sightLine = Physics2D.Raycast((Vector2)transform.position, (Vector2)player.transform.position - (Vector2)transform.position, viewDistance);

            //If the ray doesnt hit a wall, know about the player
            if (sightLine.collider.gameObject.tag != "Wall")
            {
                //If the player is in attack range, hit them. Otherwise go after them
                if (Vector2.Distance((Vector2)transform.position, (Vector2)player.transform.position) < attackDistance)
                {
                    Attack();
                }
                else
                {
                    direction = (Vector3)Arrive(player.transform.position, 2f);
                }
            }
            //Otherwise wander
            else
            {
                direction = (Vector3)Wander();
            }

        }
        //Otherwise
        else
        {
            direction = (Vector3)Wander();
        }

        transform.position += (Vector3)direction;
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

    void Attack()
    {
        if (attackTimer < 0)
        {
            weapon.SetActive(true);
            weapon.GetComponent<SpearScript>().Attack();
            attackTimer = 2f;
        }
        attackTimer -= Time.deltaTime;
    }
}
