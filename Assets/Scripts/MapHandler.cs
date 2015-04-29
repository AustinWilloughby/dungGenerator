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
    }

    // Update is called once per frame
    void Update()
    {
        CheckCurrentCell();
        if (holeCell == null)
        {
            GetHoleCell();
        }
    }

    private void CheckCurrentCell() //Determines the current player cell and adds it to the list of visited cells if needed
    {
        dungeon = GameObject.Find("Dungeon(Clone)").GetComponent<Dungeon>();    

        int playerX = (int)(player.transform.position.x + (dungeon.cellScale / 2)); 
        int playerY = (int)(player.transform.position.y + (dungeon.cellScale / 2));

        playerX = playerX / dungeon.cellScale;
        playerY = playerY / dungeon.cellScale;

        IntVector2 coords = new IntVector2(playerX, playerY);
        if (dungeon.ContainsCoords(coords))
        {
            DungeonCell currentCell = dungeon.GetCell(coords);

            if (!visitedCells.Contains(currentCell))
            {
                visitedCells.Add(currentCell);
            }
        }
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
        GetHoleCell();
    }

    private void GetHoleCell()
    {
        GameObject hole = GameObject.Find("EmptyDungeonHole");

        int holeX = (int)(hole.transform.position.x + (dungeon.cellScale / 2));
        int holeY = (int)(hole.transform.position.y + (dungeon.cellScale / 2));

        holeX = holeX / dungeon.cellScale;
        holeY = holeY / dungeon.cellScale;

        IntVector2 coords = new IntVector2(holeX, holeY);
        if (dungeon.ContainsCoords(coords))
        {
            holeCell = dungeon.GetCell(coords);

            //if (!visitedCells.Contains(holeCell))
            //{
            //    visitedCells.Add(holeCell);
            //}
        }
    }

}
