using UnityEngine;
using System.Collections;

public class DungeonCell : MonoBehaviour
{
    //Fields
    //Public
    public IntVector2 coordinates;
    public DungeonRoom room;

    //Private
    private DungeonCellEdge[] edges = new DungeonCellEdge[DungeonDirections.Count];
    private int initializedEdgeCount;

    //Methods
    public void Initialize(DungeonRoom room)
    {
        room.Add(this);
        transform.GetChild(0).GetComponent<Renderer>().material = room.setting.floorMaterial;
    }

    public DungeonCellEdge GetEdge(DungeonDirection direction)
    {
        return edges[(int)direction];
    }

    public void SetEdge(DungeonDirection direction, DungeonCellEdge edge)
    {
        edges[(int)direction] = edge;
        initializedEdgeCount += 1;
    }

    public bool IsFullyInitialized
    {
        get 
        {
            return initializedEdgeCount == DungeonDirections.Count;
        }
    }

    public DungeonDirection RandomUninitializedDirection
    {
        get
        {
            int skips = Random.Range(0, DungeonDirections.Count - initializedEdgeCount);
            for (int i = 0; i < DungeonDirections.Count; i++)
            {
                if (edges[i] == null)
                {
                    if (skips == 0)
                    {
                        return (DungeonDirection)i;
                    }
                    skips -= 1;
                }
            }
            throw new System.InvalidOperationException("The cell is fully initialized");
        }
    }
}
