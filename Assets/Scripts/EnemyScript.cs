using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyScript : Vehicle
{
    //Fields
    //Public
    public float viewDistance = 15f;
    public float attackDistance = 3f;
    public float attackTimer = 2f;
    public SpawnerHandler spawner;
    public Vector2 direction;
    public Vector2 obstacleAvoidVec;
    public LayerMask visibleLayers;
    public GameObject coinPrefab;
    public bool playerSeenLast = false;

    //Private
    private GameObject player;
    private StatTracker stats;
    private GameObject weapon;
    private Vector2 playerSeenLoc = Vector2.zero;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stats = this.GetComponent<StatTracker>();
        direction = Vector2.zero;
        obstacleAvoidVec = Vector2.zero;
        weapon = transform.GetChild(0).gameObject;
    }
        
    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance((Vector2)transform.position, (Vector2)player.transform.position) < 25 && Time.timeScale > 0)
        {
            direction = Vector2.zero;
            //obstacleAvoidVec = Vector2.zero;
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
            direction += (Vector2)Wander();
        }
        direction.Normalize();
        direction *= speed;
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
                coinDrop.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 7.9f);
            }
            GameObject.Destroy(gameObject);
        }
    }

    void Attack() //Handles attacking
    {
        if (attackTimer < 0)
        {
            weapon.GetComponent<BoxCollider2D>().enabled = true;
            weapon.GetComponent<SpearScript>().Attack();
            attackTimer = 2f;
        }
        if (attackTimer < 1)
        {
            weapon.GetComponent<BoxCollider2D>().enabled = false;
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
            if (!playerSeenLast)
            {
                playerSeenLoc = GetLastPosition().transform.position;
            }
            //If the player is in attack range, hit them. Otherwise go after them
            if (Vector2.Distance((Vector2)transform.position, (Vector2)player.transform.position) < attackDistance)
            {
                SpriteRotator(player.transform.position - transform.position);
                Attack();
            }
            else
            {
                direction += (Vector2)Arrive(player.transform.position, 2f);
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
                    direction += (Vector2)Wander();
                }
                else
                {
                    direction += (Vector2)Seek(playerSeenLoc);
                }
            }
            else
            {
                direction += (Vector2)Wander();
            }
        }

    }

    private DungeonCell GetLastPosition() //Attempts to get players last seen cell
    {
        playerSeenLast = true;
        Dungeon dungeon = GameObject.Find("Dungeon(Clone)").GetComponent<Dungeon>();

        int playerX = (int)(player.transform.position.x + (dungeon.cellScale / 2));
        int playerY = (int)(player.transform.position.y + (dungeon.cellScale / 2));

        playerX = playerX / dungeon.cellScale;
        playerY = playerY / dungeon.cellScale;

        IntVector2 coords = new IntVector2(playerX, playerY);
        if (dungeon.ContainsCoords(coords))
        {
            return dungeon.GetCell(coords);
        }
        return null;
    }
}
