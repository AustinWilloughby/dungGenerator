using UnityEngine;
using System.Collections;

public abstract class DungeonCellEdge : MonoBehaviour
{
    //Fields
    //Public 
    public DungeonCell cell;
    public DungeonCell otherCell;

    public DungeonDirection direction;

    //Methods
    public void Initialize(DungeonCell cell, DungeonCell otherCell, DungeonDirection direction)
    {
        this.cell = cell;
        this.otherCell = otherCell;
        this.direction = direction;
        cell.SetEdge(direction, this);
        transform.parent = cell.transform;
        transform.localPosition = Vector2.zero;
    }
}
