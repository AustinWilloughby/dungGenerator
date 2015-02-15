using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyScript : Vehicle
{
    //Fields
    //Public
    public float viewDistance = 15f;
    public float attackDistance = 2f;
    public float attackTimer = 2f;
    public SpawnerHandler spawner;
    public Vector2 direction;
    public LayerMask visibleLayers;
    public GameObject coinPrefab;

    //Private
    private GameObject player;
    private StatTracker stats;
    private GameObject weapon;
    private bool playerSeenLast = false;
    private Vector2 playerSeenLoc = Vector2.zero;
    private Dungeon dungeon;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        dungeon = GameObject.Find("Dungeon(Clone)").GetComponent<Dungeon>();
        stats = this.GetComponent<StatTracker>();
        direction = Vector2.zero;
        weapon = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance((Vector2)transform.position, (Vector2)player.transform.position) < 25 && Time.timeScale > 0)
        {
            direction = Vector2.zero;
            MovementHandler();
            SpriteRotator(direction);
            DeathCheck();
        }
    }

    //Methods
    void MovementHandler() //Handles all movements made by the enemy
    {
        if (Vector2.Distance((Vector2)transform.position, (Vector2)player.transform.position) < viewDistance)
        {
            ChasePlayer();
        }
        //Otherwise
        else
        {
            playerSeenLast = false;
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
            if (Random.Range(0, 0) == 0)
            {
                GameObject coinDrop = (GameObject)Instantiate(coinPrefab);
                coinDrop.GetComponent<CollectableScript>().value = Random.Range(1, 3);
                coinDrop.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
            }
            GameObject.Destroy(gameObject);
        }
    }

    void Attack() //Handles attacking
    {
        if (attackTimer < 0)
        {
            weapon.SetActive(true);
            weapon.GetComponent<SpearScript>().Attack();
            attackTimer = 2f;
        }
        attackTimer -= Time.deltaTime;
    }

    void ChasePlayer()
    {
        //Cast a ray towards the player
        RaycastHit2D sightLine = Physics2D.Raycast((Vector2)transform.position, (Vector2)player.transform.position - (Vector2)transform.position, viewDistance, visibleLayers);

        //If the ray doesnt hit a wall, know about the player
        if (sightLine.collider.gameObject.tag != "Wall")
        {
            playerSeenLast = true;
            playerSeenLoc = GetLastCell(player.transform.position);
            //If the player is in attack range, hit them. Otherwise go after them
            if (Vector2.Distance((Vector2)transform.position, (Vector2)player.transform.position) < attackDistance)
            {
                SpriteRotator(player.transform.position - transform.position);
                Attack();
            }
            else
            {
                direction = (Vector3)Arrive(player.transform.position, 2f);
            }
        }
        //Otherwise seek LastSeenPos or wander
        else
        {
            if (playerSeenLast)
            {
                if (Vector2.Distance(transform.position, playerSeenLoc) < .2f)
                {
                    playerSeenLast = false;
                    direction = (Vector3)Wander();
                }
                else
                {
                    direction = (Vector3)Seek(playerSeenLoc);
                }
            }
            else
            {
                direction = (Vector3)Wander();
            }
        }

    }

    private Vector2 GetLastCell(Vector2 pos) //Attempts to get players last seen cell
    {
        if (((int)(pos.x / dungeon.cellScale)) < (pos.x / dungeon.cellScale))
        {
            if (((pos.x / dungeon.cellScale) - ((int)(pos.x / dungeon.cellScale))) > 2.5)
            {
                pos.x += 5f;
            }
        }
        if (((int)(pos.y / dungeon.cellScale)) < (pos.y / dungeon.cellScale))
        {
            if (((pos.y / dungeon.cellScale) - ((int)(pos.y / dungeon.cellScale))) > 2.5)
            {
                pos.y += 5f;
            }
        }
        //Return the cell at those coords
        IntVector2 currentCoords = new IntVector2((int)(pos.x / dungeon.cellScale), (int)(pos.y / dungeon.cellScale));
        GameObject currentTargetCell = dungeon.GetCell(currentCoords).gameObject;
        return (Vector2)currentTargetCell.transform.position;
    }
}
