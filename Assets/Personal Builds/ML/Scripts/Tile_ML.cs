using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    Rock1, Rock2
}

public enum TileFunction
{
    Edge, Corner, Door
}

public class Coordinates
{
    public int X, Y, Z;

    public Coordinates(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }
    public Coordinates(int x, int y)
    {
        X = x;
        Y = y;
        Z = 0;
    }

}

public class Tile_ML
{
    public TileType TileType;
    public TileFunction TileFunction;
    public Coordinates Coordinates;


}
