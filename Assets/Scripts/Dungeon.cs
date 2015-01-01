using UnityEngine;
using System.Collections;

public class Dungeon : MonoBehaviour
{
    //Fields
    //Public
    public int xSize;
    public int ySize;
    public float cellScale;
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
    public void Generate()
    {
        cells = new DungeonCell[xSize, ySize];
        print("yolo");
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                CreateCell(x, y);
            }
        }
    }

    public void CreateCell(int x, int y)
    {
        DungeonCell tempCell = Instantiate(cellPrefab) as DungeonCell;
        cells[x, y] = tempCell;
        tempCell.name = "Maze Cell " + x + ", " + y;
        tempCell.transform.parent = transform;
        tempCell.transform.localPosition = new Vector3(x * cellScale, y * cellScale, 0);
    }
}
