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
           var tile = tiles[rand].transform;
           var pos = tiles[rand].gameObject.transform.TransformPoint(tile.position);
           var tempVase = Instantiate(testSpawn, tile.position, Quaternion.identity);
       }
    }

    void Update()
    {

    }
}
