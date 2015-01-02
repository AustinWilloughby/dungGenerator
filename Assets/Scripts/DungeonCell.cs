using UnityEngine;
using System.Collections;

public class DungeonCell : MonoBehaviour
{
    //Fields
    //Public
    public IntVector2 coordinates;

    //Private
    private DungeonCellEdge[] edges = new DungeonCellEdge[DungeonDirections.Count];

    //Methods
    public DungeonCellEdge GetEdge(DungeonDirection direction)
    {
        return edges[(int)direction];
    }

    public void SetEdge(DungeonDirection direction, DungeonCellEdge edge)
    {
        edges[(int)direction] = edge;
    }
}
