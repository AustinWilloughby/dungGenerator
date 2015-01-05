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
        transform.localScale = new Vector3(5, 5, 1);
    }

    public DungeonCell CreateCell(IntVector2 coords)
    {
        DungeonCell tempCell = Instantiate(cellPrefab) as DungeonCell;
        cells[coords.x, coords.y] = tempCell;
        tempCell.coordinates = coords;
        tempCell.name = "Dungeon Cell " + coords.x + ", " + coords.y;
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
        return coords.x >= 0 && coords.x < size.x && coords.y >= 0 && coords.y < size.y;
    }

    private void DoFirstGenStep(List<DungeonCell> activeCells)
    {
        DungeonCell cell = CreateCell(RandomCoordinates);
        cell.Initialize(CreateRoom(-1));
        activeCells.Add(cell);
    }

    private void DoNextGenStep(List<DungeonCell> activeCells)
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
        if (ContainsCoords(coordinates))
        {
            DungeonCell neighbor = GetCell(coordinates);
            if (neighbor == null)
            {
                neighbor = CreateCell(coordinates);
                CreatePassage(currentCell, neighbor, direction);
                activeCells.Add(neighbor);
            }
            else if (currentCell.room = neighbor.room)
            {
                CreatePassageInSameRoom(currentCell, neighbor, direction);
            }
            else
            {
                CreateWall(currentCell, neighbor, direction);
            }
        }
        else
        {
            CreateWall(currentCell, null, direction);
        }
    }

    private void CreatePassage(DungeonCell cell, DungeonCell otherCell, DungeonDirection direction)
    {
        DungeonPassage prefab = Random.value < doorProbability ? doorPrefab : passagePrefab;
        DungeonPassage passage = Instantiate(prefab) as DungeonPassage;
        passage.Initialize(cell, otherCell, direction);
        passage = Instantiate(prefab) as DungeonPassage;
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


    private void CreatePassageInSameRoom(DungeonCell cell, DungeonCell otherCell, DungeonDirection direction)
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
        if (otherCell != null)
        {
            wall = Instantiate(wallPrefab) as DungeonWall;
            wall.Initialize(otherCell, cell, direction.GetOpposite());
        }
    }

    private DungeonRoom CreateRoom(int indexToExclude)
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
}
