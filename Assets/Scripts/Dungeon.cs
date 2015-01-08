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
    public DungeonDoor doorPrefab;
    public DungeonRoomSettings[] roomSettings;

    [Range(0f, 1f)]
    public float doorProbability;

    //Private
    private DungeonCell[,] cells;
    private List<DungeonRoom> rooms = new List<DungeonRoom>();


    //Methods
    public void Generate() //Main method for dungeon generation
    {
        cells = new DungeonCell[size.x, size.y];
        List<DungeonCell> activeCells = new List<DungeonCell>();
        DoFirstGenStep(activeCells);
        while (activeCells.Count > 0)
        {
            DoNextGenStep(activeCells);
        }
        CleanDungeon();
        transform.localScale = new Vector3(5, 5, 1);
    }

    public DungeonCell GetCell(IntVector2 coords) //Returns the cell at the given coordinates
    {
        return cells[coords.x, coords.y];
    }

    public DungeonCell CreateCell(IntVector2 coords) //Creates an individual cell of the dungeon
    {
        DungeonCell tempCell = Instantiate(cellPrefab) as DungeonCell;
        cells[coords.x, coords.y] = tempCell;
        tempCell.coordinates = coords;
        tempCell.name = "Dungeon Cell " + coords.x + ", " + coords.y;
        tempCell.transform.parent = transform;
        tempCell.transform.localPosition = new Vector3(coords.x, coords.y, 0);
        return tempCell;
    }

    public IntVector2 RandomCoordinates //Generates random coordinates within the cell array
    {
        get
        {
            return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.y));
        }
    }

    public bool ContainsCoords(IntVector2 coords) //Check to make sure coords exist in the array
    {
        return coords.x >= 0 && coords.x < size.x && coords.y >= 0 && coords.y < size.y;
    }

    private void DoFirstGenStep(List<DungeonCell> activeCells) //Generates the first cell and room
    {
        DungeonCell cell = CreateCell(RandomCoordinates);
        cell.Initialize(CreateRoom(-1));
        activeCells.Add(cell);
    }

    private void DoNextGenStep(List<DungeonCell> activeCells) //Handles all other cell and wall generations and room additions
    {
        int currentIndex = activeCells.Count - 1;
        DungeonCell currentCell = activeCells[currentIndex];
        if (currentCell.IsFullyInitialized)
        {
            activeCells.RemoveAt(currentIndex);
            return;
        }
        DungeonDirection direction = currentCell.RandomUninitializedDirection;
        IntVector2 coordinates = currentCell.coordinates + direction.ToIntVec2();
        if (ContainsCoords(coordinates)) //If the coords are valid
        {
            DungeonCell neighbor = GetCell(coordinates);
            if (neighbor == null) //If its neighbor hasnt been generated
            {
                neighbor = CreateCell(coordinates);
                CreatePassage(currentCell, neighbor, direction);
                activeCells.Add(neighbor);
            }
            else if (currentCell.room.settingIndex == neighbor.room.settingIndex) //If the neighbor is in the same room
            {
                CreatePassageInSameRoom(currentCell, neighbor, direction);
            }
            else //Otherwise
            {
                CreateWall(currentCell, neighbor, direction);
            }
        }
        else //If it is on the edge
        {
            CreateWall(currentCell, null, direction);
        }
    }

    private void CreatePassage(DungeonCell cell, DungeonCell otherCell, DungeonDirection direction) //Creates a passage between cells
    {
        DungeonPassage prefab = Random.value < doorProbability ? doorPrefab : passagePrefab;
        DungeonPassage passage = Instantiate(prefab) as DungeonPassage;
        passage.Initialize(cell, otherCell, direction);
        if (passage is DungeonDoor)
        {
            otherCell.Initialize(CreateRoom(cell.room.settingIndex));
        }
        else
        {
            otherCell.Initialize(cell.room);
        }
        passage.Initialize(otherCell, cell, direction.GetOpposite());
    }


    private void CreatePassageInSameRoom(DungeonCell cell, DungeonCell otherCell, DungeonDirection direction) //Combines rooms into larger rooms
    {
        DungeonPassage passage = Instantiate(passagePrefab) as DungeonPassage;
        passage.Initialize(cell, otherCell, direction);
        passage.Initialize(otherCell, cell, direction.GetOpposite());
        if (cell.room != otherCell.room)
        {
            DungeonRoom roomToCombine = otherCell.room;
            cell.room.Combine(roomToCombine);
            rooms.Remove(roomToCombine);
            Destroy(roomToCombine);
        }
    }


    private void CreateWall(DungeonCell cell, DungeonCell otherCell, DungeonDirection direction) //Creates a wall between two cells
    {
        DungeonWall wall = Instantiate(wallPrefab) as DungeonWall;
        wall.Initialize(cell, otherCell, direction);
        if (otherCell != null)
        {
            wall.Initialize(otherCell, cell, direction.GetOpposite());
        }
    }

    private DungeonRoom CreateRoom(int indexToExclude) //Creates a room
    {
        DungeonRoom newRoom = ScriptableObject.CreateInstance<DungeonRoom>();
        newRoom.settingIndex = Random.Range(0, roomSettings.Length);
        if (newRoom.settingIndex == indexToExclude)
        {
            newRoom.settingIndex = (newRoom.settingIndex + 1) % roomSettings.Length;
        }
        newRoom.setting = roomSettings[newRoom.settingIndex];
        rooms.Add(newRoom);
        return newRoom;
    }

    private void CleanDungeon() //Removes single cell rooms from the dungeon
    {
        List<DungeonRoom> combineThese = new List<DungeonRoom>();
        List<DungeonRoom> intoThese = new List<DungeonRoom>();
        foreach (DungeonRoom room in rooms)
        {
            if (room.Cells.Count == 1) //If it is a 1 cell room
            {
                IntVector2[] neighbors = room.Cells[0].GetNeighborCoords();
                DungeonRoom surroundRoom = null;
                for (int i = 0; i < neighbors.Length; i++)
                {
                    //If it is surrounded on all sides by the same room
                    if (ContainsCoords(neighbors[i]))
                    {
                        if (surroundRoom == null)
                        {
                            if (GetCell(neighbors[i]).room != room)
                            {
                                surroundRoom = GetCell(neighbors[i]).room;
                            }
                        }
                        else if (surroundRoom != GetCell(neighbors[i]).room)
                        {
                            break;
                        }
                        if (i == neighbors.Length - 1)
                        {
                            room.setting = surroundRoom.setting;
                            room.settingIndex = surroundRoom.settingIndex;
                            room.Cells[0].CombineIntoRoom();
                            combineThese.Add(room);
                            intoThese.Add(GetCell(neighbors[i]).room);
                        }
                    }
                }
            }
        }
        //Combines island room into surrounding room
        DungeonRoom[] combineArray = combineThese.ToArray();
        for (int i = 0; i < intoThese.Count; i++)
        {
            intoThese[i].Combine(combineArray[i]);
            rooms.Remove(combineArray[i]);
            Destroy(combineArray[i]);
        }
    }
}

