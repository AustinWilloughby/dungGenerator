﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossScript : Vehicle
{
    //Fields
    //Public
    public Vector2 direction;
    public float viewDistance;
    public float viewAngle;
    public LayerMask visibleLayers;
    public DungeonCell holeCell;

    //Private
    private bool playerVisible;
    private bool alive;
    private GameObject currentTargetCell;
    private GameObject currentStartCell;
    private GameObject player;
    private StatTracker stats;
    private Dungeon dungeon;
    private List<GameObject> cellList;
    private GameObject sword;
    private GameObject sheath;
    private bool attacking;
    private float attackTimer = .01f;
    private float attackDelay = 0f;


    //Events
    // Use this for initialization
    void Start()
    {
        alive = false;
        playerVisible = false;
        player = GameObject.FindGameObjectWithTag("Player");
        stats = this.GetComponent<StatTracker>();
        direction = Vector2.zero;
        sword = GameObject.Find("BossSword");
        sheath = GameObject.Find("BossSheath");
        sword.GetComponent<Renderer>().enabled = false;
        attacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive && Time.timeScale > 0)
        {
            direction = Vector3.zero;
            SwordHandler();
            CheckDistances();
            MovementHandler();
            SpriteRotator(direction);
            DeathCheck();
            CheckViewFrustrum();
        }
    }


    //Methods
    public void Setup(Dungeon dungeon) //Collects all necessary information and sets the boss up for the level
    {
        alive = true;
        this.dungeon = dungeon;
        IntVector2 coords;
        float distToPlayer;
        do
        {
            coords = dungeon.RandomCoordinates;
            transform.position = new Vector3(coords.x * dungeon.cellScale, coords.y * dungeon.cellScale, 98);
            distToPlayer = Vector2.Distance((Vector2)player.transform.position, (Vector2)transform.position);
        } while (distToPlayer < 100f);

        currentStartCell = dungeon.GetCell(coords).gameObject;
        currentTargetCell = currentStartCell;
        GameObject emptyDungeonHole = GameObject.Find("EmptyDungeonHole");
        holeCell = dungeon.GetCell(new IntVector2((int)emptyDungeonHole.transform.position.x / dungeon.cellScale,
            (int)emptyDungeonHole.transform.position.y / dungeon.cellScale));
        cellList = new List<GameObject>();
    }

    private void DeathCheck() //Checks if the boss is dead
    {
        //If their health drops to 0 or below, kill them
        if (stats.health <= 0)
        {
            stats.health = 20;
            alive = false;
            transform.position = new Vector2(-20, -20);
        }
    }

    private void MovementHandler() //Moves the boss around based on different influences
    {
        if (playerVisible == false)
        {
            direction = Seek((Vector2)currentTargetCell.transform.position);
        }
        else
        {
            direction = Arrive((Vector2)player.transform.position, 3f);
        }
        transform.position += (Vector3)direction;
    }

    private void GetNewTarget() //Acquires a new local target based on the current cell
    {
        List<GameObject> children = new List<GameObject>();
        for (int i = 0; i < currentTargetCell.transform.childCount; i++)
        {
            if (currentTargetCell.transform.GetChild(i).gameObject.name == "DungeonPassage(Clone)" ||
                currentTargetCell.transform.GetChild(i).gameObject.name == "Dungeon Door(Clone)")
            {
                children.Add(currentTargetCell.transform.GetChild(i).gameObject);
            }
        }

        if (children.Count > 1)
        {
            List<GameObject> removalList = new List<GameObject>();
            foreach (GameObject passage in children)
            {
                if (passage.name == "DungeonPassage(Clone)")
                {
                    //If the attempted passage brings player back to starting point, into the hole, or somewhere already visited
                    if (passage.GetComponent<DungeonPassage>().otherCell == currentStartCell 
                        || passage.GetComponent<DungeonPassage>().otherCell == holeCell
                        || cellList.Contains(passage.GetComponent<DungeonPassage>().otherCell.gameObject))
                    {
                        removalList.Add(passage);
                    }
                }
                    //Same but with doors
                else if (passage.name == "Dungeon Door(Clone)")
                {
                    if (passage.GetComponent<DungeonDoor>().otherCell == currentStartCell 
                        || passage.GetComponent<DungeonDoor>().otherCell == holeCell
                        || cellList.Contains(passage.GetComponent<DungeonDoor>().otherCell.gameObject))
                    {
                        removalList.Add(passage);
                    }
                }
            }
            //Remove invalid moves from possible moves
            for (int i = 0; i < removalList.Count; i++)
            {
                children.Remove(removalList[i]);
            }
            //If removal clears out possibility list, clear visited list, and target current cell so the program continues
            if (children.Count == 0)
            {
                cellList.Clear();
                children.Add(currentStartCell);
            }
        }

        currentStartCell = currentTargetCell;
        GameObject newTarget = children[Random.Range(0, children.Count)];
        if (newTarget.name == "DungeonPassage(Clone)")
        {
            currentTargetCell = newTarget.GetComponent<DungeonPassage>().otherCell.gameObject;
        }
        else if (newTarget.name == "Dungeon Door(Clone)")
        {
            currentTargetCell = newTarget.GetComponent<DungeonDoor>().otherCell.gameObject;
        }
        cellList.Add(currentTargetCell);

    }

    private void CheckDistances() //Handles new targeting at specific points to prevent impossibilities
    {
        if (Vector2.Distance((Vector2)transform.position, (Vector2)currentTargetCell.transform.position) < .75f)
        {
            GetNewTarget();
        }
        if (playerVisible && Vector2.Distance((Vector2)transform.position, (Vector2)player.transform.position) < 4f)
        {
            Attack();
        }
    }

    private void CheckViewFrustrum() //Checks the boss' view frustrum to see if the player is visible
    {
        bool preVis = playerVisible;
        Vector2 bossToPlayer = (Vector2)player.transform.position - (Vector2)transform.position;
        if (bossToPlayer.magnitude < viewDistance) //If boss is close to player
        {
            speed = .05f;
            if (Vector2.Angle(direction, bossToPlayer) < viewAngle)
            {
                RaycastHit2D sightLine = Physics2D.Raycast((Vector2)transform.position,
                    (Vector2)player.transform.position - (Vector2)transform.position, viewDistance, visibleLayers);

                if (sightLine.collider.gameObject.tag != "Wall")
                {
                    playerVisible = true;
                }
                else
                {
                    playerVisible = false;
                    if (preVis == true)
                    {
                        TargetCurrentCell(player.transform.position);
                    }
                }
            }
            else
            {
                playerVisible = false;
                if (preVis == true)
                {
                    TargetCurrentCell(transform.position);
                }
            }
        }
        else
        {
            speed = .15f;
            playerVisible = false;
            if (preVis == true)
            {
                TargetCurrentCell(transform.position);
            }
        }
    }

    private void TargetCurrentCell(Vector3 pos) //Makes boss target cell he is currently on, to prevent running through walls when losing the player
    {
        if (((pos.x + ((float)dungeon.cellScale / 2f) / (float)dungeon.cellScale) - (int)(pos.x + ((float)dungeon.cellScale / 2f) / (float)dungeon.cellScale)) > 0)
        {
            pos.x = (int)((pos.x + ((float)dungeon.cellScale / 2f) / (float)dungeon.cellScale)) + 1;
        }
        else
        {
            pos.x = (int)((pos.x + ((float)dungeon.cellScale / 2f) / (float)dungeon.cellScale));
        }

        if (((pos.y + ((float)dungeon.cellScale / 2f) / (float)dungeon.cellScale) - (int)(pos.y + ((float)dungeon.cellScale / 2f) / (float)dungeon.cellScale)) > 0)
        {
            pos.y = (int)((pos.y + ((float)dungeon.cellScale / 2f) / (float)dungeon.cellScale)) + 1;
        }
        else
        {
            pos.y = (int)((pos.y + ((float)dungeon.cellScale / 2f) / (float)dungeon.cellScale));
        }
        IntVector2 currentCoords = new IntVector2((int)(pos.x / dungeon.cellScale), (int)(pos.y / dungeon.cellScale));
        currentTargetCell = dungeon.GetCell(currentCoords).gameObject;
    }

    private void Attack()
    {
        if (attacking == false && attackDelay <= 0)
        {
            attacking = true;
            sword.SetActive(true);
            sword.GetComponent<Renderer>().enabled = true;
            attackTimer = .375f;
        }
    }

    private void SwordHandler()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
            //Rotates sword in an arc in front of boss
            sword.transform.RotateAround(transform.position, Vector3.forward, (270 * Time.deltaTime));
            if (attackTimer < 0)
            {
                attacking = false;
                //Moves sword back to starting position
                sword.transform.position = sheath.transform.position;
                sword.transform.rotation = sheath.transform.rotation;
                sword.GetComponent<Renderer>().enabled = false;
                sword.SetActive(false);
                attackDelay = 1;
            }
        }
        else
        {
            attackDelay -= Time.deltaTime;
        }
    }
}
