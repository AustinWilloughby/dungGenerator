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
    private List<DungeonCell> cells = new List<DungeonCell>();


    //Methods
    public void Add(DungeonCell cell)
    {
        cell.room = this;
        cells.Add(cell);
    }
}
