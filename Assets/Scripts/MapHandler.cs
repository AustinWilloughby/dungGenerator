using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapHandler : MonoBehaviour
{
    //Fields
    //Private
    private List<DungeonCell> visitedCells;
    private GameObject player;
    private Dungeon dungeon;

    // Use this for initialization
    void Start()
    {
        visitedCells = new List<DungeonCell>();
        player = GameObject.Find("Player");
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
}
