using UnityEngine;
using System.Collections;

public enum DungeonDirection //Possible generation directions
{
    North,
    East,
    South,
    West
};


public static class DungeonDirections
{
    public const int Count = 4;

    public static DungeonDirection RandValue //Random direction
    {
        get
        {
            return (DungeonDirection)Random.Range(0, Count);
        }
    }

    //Intvector2 representations of the 4 directions
    private static IntVector2[] vectors = 
    {
        new IntVector2(0,1),
        new IntVector2(1,0),
        new IntVector2(0,-1),
        new IntVector2(-1,0)
    };

    //Opposite directions
    private static DungeonDirection[] opposites =
    {
        DungeonDirection.South,
        DungeonDirection.West,
        DungeonDirection.North,
        DungeonDirection.East
    };

    //Rotations from the identity representing the directions
    private static Quaternion[] rotations = 
    {
        Quaternion.identity,
        Quaternion.Euler(0f, 0f, 270f),
        Quaternion.Euler(0f, 0f, 180f),
        Quaternion.Euler(0f, 0f, 90f)
    };

    //Converts a given direction to its respective intvect2
    public static IntVector2 ToIntVec2(this DungeonDirection direction)
    {
        return vectors[(int)direction];
    }

    //Gets the opposite direction
    public static DungeonDirection GetOpposite(this DungeonDirection direction)
    {
        return opposites[(int)direction];
    }

    //Gets the rotation
    public static Quaternion ToRotation(this DungeonDirection direction)
    {
        return rotations[(int)direction];
    }
    
}
