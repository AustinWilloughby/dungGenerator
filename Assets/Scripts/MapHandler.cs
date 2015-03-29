using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapHandler : MonoBehaviour
{
    //Fields
    //Private
    private List<DungeonCell> visitedCells;
    private DungeonCell holeCell;
    private GameObject player;
    private Dungeon dungeon;

    // Use this for initialization
    void Start()
    {
        visitedCells = new List<DungeonCell>();
        player = GameObject.Find("Player");
        //holeCell = the cell containing the hole.
    }

    // Update is called once per frame
    void Update()
    {
        CheckCurrentCell();
    }

    private void CheckCurrentCell() //Determines the current player cell and adds it to the list of visited cells if needed
    {
        dungeon = GameObject.Find("Dungeon(Clone)").GetComponent<Dungeon>();
        IntVector2 playerCoords = new IntVector2((int)((player.transform.position.x + (dungeon.cellScale / 2)) / dungeon.cellScale), 
            (int)((player.transform.position.x + (dungeon.cellScale / 2)) / dungeon.cellScale));
        if (!visitedCells.Contains(dungeon.GetCell(playerCoords)))
        {
            visitedCells.Add(dungeon.GetCell(playerCoords));
        }
    }

    //Draw map
    //Loops through tiles and determines which cells to fill on the map, including ajoined walls.
    //Then pauses the game, and draws the map to the screen.

    //Clear map
    //If dungeon level changes, clear the map and find the hole location again.

}
