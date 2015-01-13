﻿using UnityEngine;
using System.Collections;

public class DungeonPopulator : MonoBehaviour
{
    //Fields
    //Public
    public GameObject ropePrefab;
    public GameObject[] collectables;

    //Private
    private Dungeon dungeon;
    private GameObject player; 
    private GameObject ropelessHole;
    private GameObject collectableHolder;


    //Methods
    public void Populate() //Main method for populating every entity into the dungeon
    {
        GetInfo();
        PlaceRopeAndHole();
        PlaceCollectables();
    }

    private void GetInfo() //Gets field info in place of a start event
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ropelessHole = GameObject.Find("EmptyDungeonHole");
        dungeon = gameObject.GetComponent<Dungeon>();
        collectableHolder = GameObject.Find("Collectables");
    }

    private void PlaceRopeAndHole() //Handles placing the rope and hole in the dungeon
    {
        EnsureHoleSpace();
        bool looper = true;
        GameObject rope = (GameObject)Instantiate(ropePrefab);
        int failCounter = 0; //Prevents program from getting stuck
        do //Make a random location, and ensure it is far from player and hole
        {
            int xLoc = (int)Random.Range(0, dungeon.size.x) * dungeon.cellScale;
            int yLoc = (int)Random.Range(0, dungeon.size.y) * dungeon.cellScale;
            rope.transform.position = new Vector3(xLoc, yLoc, 29);
            if (failCounter < 5)
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
            IntVector2 holeCoords = new IntVector2((int)Random.Range(0, dungeon.size.x), (int)Random.Range(0, dungeon.size.y));
            ropelessHole.transform.position = new Vector3(holeCoords.x * dungeon.cellScale, holeCoords.y * dungeon.cellScale, 30);

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
            if (failCounter < 5)
            {
                if (Vector2.Distance((Vector2)ropelessHole.transform.position, (Vector2)player.transform.position) > 75)
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

    private void PlaceCollectables()
    {
        for (int i = 0; i < 25 + (dungeon.DungeonLevel - 1); i++)
        {
            GameObject collectable = (GameObject)Instantiate(collectables[Random.Range(0, collectables.Length)]);
            IntVector2 coords = dungeon.RandomCoordinates;
            collectable.transform.position = new Vector3(coords.x * dungeon.cellScale, coords.y * dungeon.cellScale, 15);
            collectable.transform.parent = collectableHolder.transform;
            Collider2D[] colliders = Physics2D.OverlapCircleAll((Vector2)collectable.transform.position, .16f);
            if (colliders.Length > 1)
            {
                GameObject.Destroy(collectable);
                i--;
            }
        }
    }
}