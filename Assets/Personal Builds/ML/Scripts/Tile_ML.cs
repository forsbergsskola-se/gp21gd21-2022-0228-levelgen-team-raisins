using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    Floor, Wall, Corner,
}

public enum TileFunction
{
    Edge, Corner, Door
}

public enum TileContent
{
    Treasure, Enemy, Trap, None
}

[Serializable]
public class Coordinates
{
    public int X, Y, Z;

    public Coordinates(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }
    public Coordinates(int x, int z)
    {
        X = x;
        Y = 0;
        Z = z;
    }

}

[Serializable]
public class Tile_ML
{
    public TileType TileType;
    public TileFunction TileFunction;
    public TileContent TileContent;
    public Coordinates Coordinates;
    public Tile_ML[] neighbours;

}
