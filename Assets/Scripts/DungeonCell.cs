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

    public DungeonCellEdge GetEdge(DungeonDirection direction) //Returns cellEdge in a given direction
    {
        return edges[(int)direction];
    }

    public void SetEdge(DungeonDirection direction, DungeonCellEdge edge) //Sets a given cellEdge
    {
        edges[(int)direction] = edge;
        initializedEdgeCount += 1;
    }

    public bool IsFullyInitialized //Checks if all edges have been initialized
    {
        get
        {
            return initializedEdgeCount == DungeonDirections.Count;
        }
    }

    public DungeonDirection RandomUninitializedDirection //Returns a random direction that hasnt been initialized
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

    public IntVector2[] GetNeighborCoords() //Gets the coordinates of the 4 surrounding potential cells
    {
        IntVector2[] neighbors =
        {
            new IntVector2(coordinates.x + 1, coordinates.y),
            new IntVector2(coordinates.x - 1, coordinates.y),
            new IntVector2(coordinates.x, coordinates.y + 1),
            new IntVector2(coordinates.x, coordinates.y - 1)
        };
        return neighbors;
    }

    public void CombineIntoRoom() //Sets cells material to match the rooms
    {
        transform.GetChild(0).GetComponent<Renderer>().material = room.setting.floorMaterial;
    }
}