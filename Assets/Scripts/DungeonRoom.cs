using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonRoom : ScriptableObject
{
    //Fields
    //Public
    public int settingIndex;
    public DungeonRoomSettings setting;

    //Private
    private List<DungeonCell> cells = new List<DungeonCell>(); //cells contained in the dungeon

    //Attributes
    public List<DungeonCell> Cells
    {
        get { return cells; }
    }

    //Methods
    public void Add(DungeonCell cell) //Add a cell to the room
    {
        cell.room = this;
        cells.Add(cell);
    }

    public void Combine(DungeonRoom room) //Combines two rooms
    {
        for (int i = 0; i < room.cells.Count; i++)
        {
            Add(room.cells[i]);
        }
    }
}
