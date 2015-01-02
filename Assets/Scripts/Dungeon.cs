using UnityEngine;
using System.Collections;

public class Dungeon : MonoBehaviour
{
    //Fields
    //Public
    public IntVector2 size;
    public DungeonCell cellPrefab;

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

        IntVector2 coords = RandomCoordinates();
        while (ContainsCoords(coords) && GetCell(coords) == null)
        {
            CreateCell(coords);
            coords += MazeDirections.RandValue.ToIntVec2();
        }
    }

    public void CreateCell(IntVector2 coords)
    {
        DungeonCell tempCell = Instantiate(cellPrefab) as DungeonCell;
        cells[coords.x, coords.y] = tempCell;
        tempCell.coordinates = coords;
        tempCell.name = "Maze Cell " + coords.x + ", " + coords.y;
        tempCell.transform.parent = transform;
        tempCell.transform.localPosition = new Vector3(coords.x, coords.y, 0);
    }

    public IntVector2 RandomCoordinates()
    {
        return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.y) );
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
}
