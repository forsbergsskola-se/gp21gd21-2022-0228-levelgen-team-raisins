using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public enum AssetType
{
    Breakable, Enemy, Treasure
}

public class RoomTest_ML : MonoBehaviour
{
    public GameObject testSpawn;
    public List<int> availableFloorTiles;
    public List<MeshFilter> availableTiles;
    private NavMeshSurface navSurface;

    void Start()
    {
        availableTiles = GetTilesOfType(TileType.Floor);
        SpawnAssets(3, AssetType.Enemy);
        navSurface = GetComponent<NavMeshSurface>();


    }



    private List<MeshFilter> GetTilesOfType(TileType tileType)
    {
        string tileString = tileType switch
        {
            TileType.Wall => "Wall",
            TileType.Floor => "Floor",
            TileType.Corner => "Corner",
            TileType.Floor_Wall => "Floor_Wall",
            _ => ""
        };

        var tiles = gameObject.GetComponentsInChildren<MeshFilter>()
            .Where(x => x.CompareTag(tileString)).ToList();

        return tiles;
    }

    private void SpawnAssets(int amount, AssetType assetType)
    {
        for (int i = 0; i < amount; i++)
        {
            SpawnAsset(assetType);
        }
    }

    private void SpawnAsset(AssetType assetType)
    {
        if (availableTiles.Count < 1) return;

        var rand = Random.Range(0, availableTiles.Count - 1);

        var tilePos = availableTiles[rand].gameObject.transform.position;
        var scale = availableTiles[rand].mesh.bounds.size;
        var adjustPos = tilePos + new Vector3(scale.x / 2,0, -scale.z / 2);
        var theAsset = Instantiate(testSpawn, adjustPos, Quaternion.identity);

        if (assetType != AssetType.Enemy)
        {
            AdjustObjectPlacement(theAsset, 300);
            availableTiles.RemoveAt(rand);
        }
        else
        {

        }
    }


    private void AdjustObjectPlacement(GameObject theObject, float maxRotation)
    {
        var randRot = Random.Range(0f, maxRotation);
        theObject.transform.Rotate(new Vector3(0,1,0), randRot);

        var objectSize = theObject.GetComponent<MeshFilter>().mesh.bounds.size;
        var randMoveX = Random.Range(0f, objectSize.x / 5);
        var randMoveZ = Random.Range(0f, objectSize.z / 5);

        theObject.transform.position += new Vector3(randMoveX, 0, randMoveZ);
    }


}
