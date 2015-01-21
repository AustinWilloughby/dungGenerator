using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossScript : Vehicle
{
    //Fields
    //Public
    public Vector2 direction;

    //Private
    private GameObject currentTargetCell;
    private GameObject currentStartCell;
    private GameObject player;
    private StatTracker stats;
    private Dungeon dungeon;
    private GameObject dungeonHole;
    private GameObject emptyDungeonHole;

    //Events
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stats = this.GetComponent<StatTracker>();
        direction = Vector2.zero;
        dungeonHole = GameObject.Find("DungeonHole");
        emptyDungeonHole = GameObject.Find("EmptyDungeonHole");
    }

    // Update is called once per frame
    void Update()
    {
        direction = Vector2.zero;
        CheckDistances();
        MovementHandler();
        SpriteRotator(direction);
        DeathCheck();
    }

    //Methods

    public void Setup(Dungeon dungeon) //Collects all necessary information and sets the boss up for the level
    {
        this.dungeon = dungeon;
        IntVector2 coords;
        do
        {
            coords = dungeon.RandomCoordinates;
            transform.position = new Vector3(coords.x * dungeon.cellScale, coords.y * dungeon.cellScale, 0);
        } while (Vector2.Distance((Vector2)player.transform.position, (Vector2)transform.position) < 20f);
        currentStartCell = dungeon.GetCell(coords).gameObject;
        currentTargetCell = currentStartCell;
    }

    private void DeathCheck() //Checks if the boss is dead
    {
        //If their health drops to 0 or below, kill them
        if (stats.health <= 0)
        {
            GameObject.Destroy(gameObject);
        }
    }

    private void MovementHandler() //Moves the boss around based on different influences
    {
        //if (Vector2.Distance((Vector2)player.transform.position, (Vector2)transform.position) > 15)
        {
            direction = Seek((Vector2)currentTargetCell.transform.position);
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
            GameObject noPassage = null;
            foreach (GameObject passage in children)
            {
                if (passage.name == "DungeonPassage(Clone)")
                {
                    if (passage.GetComponent<DungeonPassage>().otherCell == currentStartCell)
                    {
                        noPassage = passage;
                    }
                }
                else if (passage.name == "Dungeon Door(Clone)")
                {
                    if (passage.GetComponent<DungeonDoor>().otherCell == currentStartCell)
                    {
                        noPassage = passage;
                    }
                }
            }
            children.Remove(noPassage);
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

    private void CheckDistances()
    {
        if (Vector2.Distance((Vector2)transform.position, (Vector2)currentTargetCell.transform.position) < 1.5f)
        {
            GetNewTarget();
        }
        if (Vector2.Distance((Vector2)transform.position, (Vector2)dungeonHole.transform.position) < 5f)
        {
            GetNewTarget();
        }
        if (Vector2.Distance((Vector2)transform.position, (Vector2)emptyDungeonHole.transform.position) < 5f)
        {
            GetNewTarget();
        }
    }
}
