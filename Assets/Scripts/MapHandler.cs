using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapHandler : MonoBehaviour
{
    //Fields
    //Public
    public bool drawing = false;

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
        GetHoleCell();
    }

    // Update is called once per frame
    void Update()
    {
        CheckCurrentCell();
    }

    private void CheckCurrentCell() //Determines the current player cell and adds it to the list of visited cells if needed
    {
        dungeon = GameObject.Find("Dungeon(Clone)").GetComponent<Dungeon>();    

        int playerX = (int)(player.transform.position.x + (dungeon.cellScale / 2)); 
        int playerY = (int)(player.transform.position.y + (dungeon.cellScale / 2));

        playerX = playerX / dungeon.cellScale;
        playerY = playerY / dungeon.cellScale;

        IntVector2 coords = new IntVector2(playerX, playerY);
        DungeonCell currentCell = dungeon.GetCell(coords);

        if (!visitedCells.Contains(currentCell))
        {
            visitedCells.Add(currentCell);
        }

        print(visitedCells.Count);
    }

    private void DrawMap()
    {
        if (drawing)
        {
            //draw each cell at appropriate location
        }
    }

    public void ClearMap()
    {
        for (int i = 0; i < visitedCells.Count; i++)
        {
            visitedCells.Remove(visitedCells[i]);
        }
        //Set new hole cell
    }

    private void GetHoleCell()
    {
        GameObject hole = GameObject.Find("DungeonHole");
        int holeX = (int)(hole.transform.position.x + (dungeon.cellScale / 2));
        int holeY = (int)(hole.transform.position.y + (dungeon.cellScale / 2));

        IntVector2 coords = new IntVector2(holeX, holeY);
        DungeonCell currentCell = dungeon.GetCell(coords);

        if (!visitedCells.Contains(currentCell))
        {
            visitedCells.Add(currentCell);
        }
    }

}
