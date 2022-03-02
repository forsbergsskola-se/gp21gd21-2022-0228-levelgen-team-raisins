using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomTest_ML : MonoBehaviour
{
    public GameObject testSpawn;
    void Start()
    {
        GetFloorTiles();
    }

    private void GetFloorTiles()
    {
       var tiles = gameObject.GetComponentsInChildren<MeshFilter>()
            .Where(x => x.CompareTag("Floor")).ToList();

       for (int i = 0; i < 5; i++)
       {
           var rand = Random.Range(0, tiles.Count);
           var tile = tiles[rand].gameObject.transform;
           var scale = tiles[rand].mesh.bounds.size;
           var adjustPos = tile.position + new Vector3(scale.x / 2,0, -scale.z / 2);
           Debug.Log(tile.position);
           var tempVase = Instantiate(testSpawn, adjustPos, Quaternion.identity);
       }
    }

}
