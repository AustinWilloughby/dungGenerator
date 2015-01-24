using UnityEngine;
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

    //Private
    private bool playerVisible;
    private bool alive;
    private GameObject currentTargetCell;
    private GameObject currentStartCell;
    private GameObject player;
    private StatTracker stats;
    private Dungeon dungeon;
    public DungeonCell holeCell;


    //Events
    // Use this for initialization
    void Start()
    {
        alive = false;
        playerVisible = false;
        player = GameObject.FindGameObjectWithTag("Player");
        stats = this.GetComponent<StatTracker>();
        direction = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            direction = Vector3.zero;
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
        do
        {
            coords = dungeon.RandomCoordinates;
            transform.position = new Vector3(coords.x * dungeon.cellScale, coords.y * dungeon.cellScale, 0);
        } while (Vector2.Distance((Vector2)player.transform.position, (Vector2)transform.position) < 20f);
        currentStartCell = dungeon.GetCell(coords).gameObject;
        currentTargetCell = currentStartCell;
        GameObject emptyDungeonHole = GameObject.Find("EmptyDungeonHole");
        holeCell = dungeon.GetCell(new IntVector2((int)emptyDungeonHole.transform.position.x / dungeon.cellScale,
            (int)emptyDungeonHole.transform.position.y / dungeon.cellScale));
    }

    private void DeathCheck() //Checks if the boss is dead
    {
        //If their health drops to 0 or below, kill them
        if (stats.health <= 0)
        {
            stats.health = 100;
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
            direction = Arrive((Vector2)player.transform.position, 2f);
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
            GameObject noPassage = null; //Prevents returning to the same cell
            GameObject passageToHole = null; //Prevents going into the dungeon hole
            foreach (GameObject passage in children)
            {
                if (passage.name == "DungeonPassage(Clone)")
                {
                    if (passage.GetComponent<DungeonPassage>().otherCell == currentStartCell)
                    {
                        noPassage = passage;
                    }
                    if (passage.GetComponent<DungeonPassage>().otherCell == holeCell)
                    {
                        passageToHole = passage;
                    }
                }
                else if (passage.name == "Dungeon Door(Clone)")
                {
                    if (passage.GetComponent<DungeonDoor>().otherCell == currentStartCell)
                    {
                        noPassage = passage;
                    }
                    if (passage.GetComponent<DungeonDoor>().otherCell == holeCell)
                    {
                        passageToHole = passage;
                    }
                }
            }
            children.Remove(noPassage);
            children.Remove(passageToHole);
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

    }

    private void CheckDistances() //Handles new targeting at specific points to prevent impossibilities
    {
        if (Vector2.Distance((Vector2)transform.position, (Vector2)currentTargetCell.transform.position) < 1.5f)
        {
            GetNewTarget();
        }
    }

    private void CheckViewFrustrum() //Checks the boss' view frustrum to see if the player is visible
    {
        Vector2 bossToPlayer = (Vector2)player.transform.position - (Vector2)transform.position;
        if (bossToPlayer.magnitude < viewDistance)
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
                }
            }
            else
            {
                playerVisible = false;
            }
        }
        else
        {
            speed = .2f;
            playerVisible = false;
        }
    }
}
