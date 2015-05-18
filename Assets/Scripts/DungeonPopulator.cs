using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonPopulator : MonoBehaviour
{
    //Fields
    //Public
    public GameObject ropePrefab;
    public GameObject potionPrefab;
    public GameObject swordPrefab;
    public GameObject[] collectables;
    public GameObject spawnerPrefab;
    public GameObject[] enemyTypes;
    public GameObject playerCamera;

    //Private
    private Dungeon dungeon;
    private GameObject player;
    private GameObject ropelessHole;
    private GameObject collectableHolder;
    private GameObject spawnerHolder;
    private GameObject boss;
    private MusicScript music;


    //Methods
    public void Populate() //Main method for populating every entity into the dungeon
    {
        GetInfo();
        PlaceRopeAndHole();
        PlaceSpawners();
        PlaceCollectables();
        PlacePotion();
        PlaceBoss();
        music.NextSong();
    }

    private void GetInfo() //Gets field info in place of a start event
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ropelessHole = GameObject.Find("EmptyDungeonHole");
        dungeon = gameObject.GetComponent<Dungeon>();
        collectableHolder = GameObject.Find("Collectables");
        spawnerHolder = GameObject.Find("Spawners");
        boss = GameObject.Find("Boss");
        playerCamera = GameObject.Find("Main Camera");
        music = playerCamera.GetComponent<MusicScript>();
    }

    private void PlaceRopeAndHole() //Handles placing the rope and hole in the dungeon
    {
        EnsureHoleSpace();
        bool looper = true;
        GameObject rope = (GameObject)Instantiate(ropePrefab);
        int failCounter = 0; //Prevents program from getting stuck
        do //Make a random location, and ensure it is far from player and hole
        {
            IntVector2 coords = dungeon.RandomCoordinates;
            rope.transform.position = new Vector3(coords.x * dungeon.cellScale, coords.y * dungeon.cellScale, 99);
            if (failCounter < 20)
            {
                if (Vector2.Distance((Vector2)rope.transform.position, (Vector2)player.transform.position) > 50)
                {
                    if (Vector2.Distance((Vector2)rope.transform.position, (Vector2)ropelessHole.transform.position) > 50)
                    {
                        looper = false;
                    }
                }
            }
            else
            {
                if (Vector2.Distance((Vector2)rope.transform.position, (Vector2)ropelessHole.transform.position) > 5)
                {
                    looper = false;
                }
            }
            failCounter++;
        } while (looper);
    }

    private void EnsureHoleSpace() //Places the dungeon hole, ensuring it is not blocking a passage
    {
        bool looper = true;
        int failCounter = 0; //Prevents program from getting stuck
        do
        {
            IntVector2 holeCoords = dungeon.RandomCoordinates;
            ropelessHole.transform.position = new Vector3(holeCoords.x * dungeon.cellScale, holeCoords.y * dungeon.cellScale, 99);

            //Get all nearby colliders
            Collider2D[] nearbyColliders = Physics2D.OverlapCircleAll((Vector2)ropelessHole.transform.position, 6f);
            bool wallCheck = true;
            for (int i = 0; i < nearbyColliders.Length; i++)
            {
                //If one is a wall, break and make a new position
                if (nearbyColliders[i].tag == "Wall")
                {
                    wallCheck = false;
                    break;
                }
            }
            if (failCounter < 5) //Prevents game from locking up
            {
                if (Vector2.Distance((Vector2)ropelessHole.transform.position, (Vector2)player.transform.position) > 50)
                {
                    if (wallCheck)
                    {
                        looper = false;
                    }
                }
            }
            else
            {
                if (wallCheck || failCounter > 2000)
                {
                    if (Vector2.Distance((Vector2)player.transform.position, (Vector2)ropelessHole.transform.position) > 5)
                    {
                        looper = false;
                    }
                }
            }
            failCounter++;
        } while (looper);
    }

    private void PlaceCollectables() //Places coins and collectables throughout the dungeon in random clusters
    {
        List<GameObject> current = new List<GameObject>();
        for (int i = 0; i < 150; i++)
        {
            int random = Random.Range(0, 2);
            GameObject collectable = (GameObject)Instantiate(collectables[0]);
            if (random == 0 || current.Count < 10) //New cloud seed coin
            {
                IntVector2 coords = dungeon.RandomCoordinates;
                collectable.transform.position = new Vector3(coords.x * dungeon.cellScale + Random.Range(-3f, 3f), coords.y * dungeon.cellScale + Random.Range(-3f, 3f), 99);
                collectable.transform.parent = collectableHolder.transform;
            }
            else //Cloud member
            {
                GameObject parentCoin = current[Random.Range(0, current.Count - 1)];
                collectable.transform.position = new Vector3(parentCoin.transform.position.x + Random.Range(-1f, 1f),
                                                            parentCoin.transform.position.y * dungeon.cellScale + Random.Range(-1f, 1f), 99);
                collectable.transform.parent = collectableHolder.transform;
            }
            Collider2D[] colliders = Physics2D.OverlapCircleAll((Vector2)collectable.transform.position, .5f);
            if(colliders.Length < 1)
            {
                //If outside, destroy and retry
                GameObject.Destroy(collectable);
                i--;
            }
            else
            {
                //Prevent overlap
                if (colliders.Length > 1)
                {
                    GameObject.Destroy(collectable);
                    i--;
                }
                else
                {
                    current.Add(collectable);
                }
            }
        }
        for (int i = 0; i < 5; i++) //Sapphires
        {
            GameObject collectable = (GameObject)Instantiate(collectables[1]);
            IntVector2 coords = dungeon.RandomCoordinates;
            collectable.transform.position = new Vector3(coords.x * dungeon.cellScale + Random.Range(-3f, 3f), coords.y * dungeon.cellScale + Random.Range(-3f, 3f), 99);
            collectable.transform.parent = collectableHolder.transform;
            Collider2D[] colliders = Physics2D.OverlapCircleAll((Vector2)collectable.transform.position, .5f);
            if (colliders.Length != 1)
            {
                GameObject.Destroy(collectable);
                i--;
            }
        }
        for (int i = 0; i < 3; i++) //Emeralds
        {
            GameObject collectable = (GameObject)Instantiate(collectables[2]);
            IntVector2 coords = dungeon.RandomCoordinates;
            collectable.transform.position = new Vector3(coords.x * dungeon.cellScale + Random.Range(-3f, 3f), coords.y * dungeon.cellScale + Random.Range(-3f, 3f), 99);
            collectable.transform.parent = collectableHolder.transform;
            Collider2D[] colliders = Physics2D.OverlapCircleAll((Vector2)collectable.transform.position, .5f);
            if (colliders.Length != 1)
            {
                GameObject.Destroy(collectable);
                i--;
            }
        }

        for (int i = 0; i < 1; i++) //Diamonds
        {
            GameObject collectable = (GameObject)Instantiate(collectables[3]);
            do
            {
                IntVector2 coords = dungeon.RandomCoordinates;
                collectable.transform.position = new Vector3(coords.x * dungeon.cellScale + Random.Range(-3f, 3f), coords.y * dungeon.cellScale + Random.Range(-3f, 3f), 99);
                collectable.transform.parent = collectableHolder.transform;
            } while (Vector2.Distance(player.transform.position, collectable.transform.position) > 30f);
            Collider2D[] colliders = Physics2D.OverlapCircleAll((Vector2)collectable.transform.position, .5f);
            if (colliders.Length != 1)
            {
                GameObject.Destroy(collectable);
                i--;
            }
        }

    }

    private void PlacePotion() //Places a number of potions based on the dungeonLevel around the level
    {
        GameObject potion = (GameObject)Instantiate(potionPrefab);
        IntVector2 coords = dungeon.RandomCoordinates;
        potion.transform.position = new Vector3(coords.x * dungeon.cellScale, coords.y * dungeon.cellScale, 99);
        potion.transform.parent = collectableHolder.transform;
        potion.transform.parent = collectableHolder.transform;

    }

    private void PlaceSpawners() //Places spawners that spawn random enemies around the dungeon
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject spawner = (GameObject)Instantiate(spawnerPrefab);
            IntVector2 coords = dungeon.RandomCoordinates;
            spawner.transform.position = new Vector3(coords.x * dungeon.cellScale, coords.y * dungeon.cellScale, 92);
            Collider2D[] colliders = Physics2D.OverlapCircleAll((Vector2)spawner.transform.position, 4f);
            if (colliders.Length > 1)
            {
                GameObject.Destroy(spawner);
                i--;
            }
            else
            {
                if (enemyTypes.Length > 0)
                {
                    spawner.GetComponent<SpawnerHandler>().enemyPrefab = enemyTypes[Random.Range(0, enemyTypes.Length)];
                }
                spawner.GetComponent<SpawnerHandler>().maxSpawns = Random.Range(3, 7);
                spawner.GetComponent<SpawnerHandler>().spawnRate = Random.Range(10f, 15f);
                spawner.transform.parent = spawnerHolder.transform;
            }
        }
    }

    private void PlaceBoss() //Places the boss somewhere in the dungeon
    {
        boss.GetComponent<BossScript>().Setup(dungeon);
    }
}
