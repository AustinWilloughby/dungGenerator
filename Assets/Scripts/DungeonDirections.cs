﻿using UnityEngine;
using System.Collections;

public enum DungeonDirection
{
    North,
    East,
    South,
    West
};


public static class DungeonDirections
{
    public const int Count = 4;

    public static DungeonDirection RandValue
    {
        get
        {
            return (DungeonDirection)Random.Range(0, Count);
        }
    }

    private static IntVector2[] vectors = 
    {
        new IntVector2(0,1),
        new IntVector2(1,0),
        new IntVector2(0,-1),
        new IntVector2(-1,0)
    };

    private static DungeonDirection[] opposites =
    {
        DungeonDirection.South,
        DungeonDirection.West,
        DungeonDirection.North,
        DungeonDirection.East
    };

    public static IntVector2 ToIntVec2(this DungeonDirection direction)
    {
        return vectors[(int)direction];
    }

    public static DungeonDirection GetOpposite(this DungeonDirection direction)
    {
        return opposites[(int)direction];
    }
    
}
