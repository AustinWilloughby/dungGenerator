using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dungeon : MonoBehaviour
{
    //Fields
    //Public
    public IntVector2 size;
    public DungeonCell cellPrefab;
    public DungeonPassage passagePrefab;
    public DungeonWall wallPrefab;

    //Private
    private DungeonCell[,] cells;


    //Events
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    //Methods
    public DungeonCell GetCell(IntVector2 coords)
    {
        return cells[coords.x, coords.y];
    }

    public void Generate()
    {
        cells = new DungeonCell[size.x, size.y];
        List<DungeonCell> activeCells = new List<DungeonCell>();

        DoFirstGenStep(activeCells);
        while (activeCells.Count > 0)
        {
            DoNextGenStep(activeCells);
        }
    }

    public DungeonCell CreateCell(IntVector2 coords)
    {
        DungeonCell tempCell = Instantiate(cellPrefab) as DungeonCell;
        cells[coords.x, coords.y] = tempCell;
        tempCell.coordinates = coords;
        tempCell.name = "Maze Cell " + coords.x + ", " + coords.y;
        tempCell.transform.parent = transform;
        tempCell.transform.localPosition = new Vector3(coords.x, coords.y, 0);
        return tempCell;
    }

    public IntVector2 RandomCoordinates
    {
        get
        {
            return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.y));
        }
    }

    public bool ContainsCoords(IntVector2 coords)
    {
        if (coords.x >= 0 && coords.x < size.x)
        {
            if (coords.y >= 0 && coords.y < size.y )
            {
                return true;
            }
        }
        return false;
    }

    private void DoFirstGenStep(List<DungeonCell> activeCells)
    {
        activeCells.Add(CreateCell(RandomCoordinates));
    }

    private void DoNextGenStep(List<DungeonCell> activeCells)
    {
        int currentIndex = activeCells.Count - 1;
        DungeonCell current = activeCells[currentIndex];
        DungeonDirection direction = DungeonDirections.RandValue;
        IntVector2 coords = current.coordinates + direction.ToIntVec2();
        if (ContainsCoords(coords))
        {
            DungeonCell neighbor = GetCell(coords);
            if (neighbor == null)
            {
                neighbor = CreateCell(coords);
                CreatePassage(current, neighbor, direction);
                activeCells.Add(neighbor);
            }
            else
            {
                CreateWall(current, neighbor, direction);
                activeCells.RemoveAt(currentIndex);
            }
        }
        else
        {
            CreateWall(current, null, direction);
            activeCells.RemoveAt(currentIndex);
        }
    }

    private void CreatePassage(DungeonCell cell, DungeonCell otherCell, DungeonDirection direction)
    {
        DungeonPassage passage = Instantiate(passagePrefab) as DungeonPassage;
        passage.Initialize(cell, otherCell, direction);
        passage = Instantiate(passagePrefab) as DungeonPassage;
        passage.Initialize(otherCell, cell, direction.GetOpposite());
    }


    private void CreateWall(DungeonCell cell, DungeonCell otherCell, DungeonDirection direction)
    {
        DungeonWall wall = Instantiate(wallPrefab) as DungeonWall;
        wall.Initialize(cell, otherCell, direction);
        if(otherCell!= null)
        {
            wall = Instantiate(wallPrefab) as DungeonWall;
            wall.Initialize(otherCell, cell, direction.GetOpposite());
        }
    }

}
