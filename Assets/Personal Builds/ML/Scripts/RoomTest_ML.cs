using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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

       List<int> randoms = new List<int>();

       for (int i = 0; i < 23; i++)
       {
           var randomNumbers = Random.Range(0, tiles.Count);
           if(!randoms.Contains(randomNumbers))
               randoms.Add(randomNumbers);
       }


       for (int i = 0; i < 5; i++)
       {
           var rand = randoms[i];
           var randRot = Random.Range(5f, 300f);
           var tilePos = tiles[rand].gameObject.transform.position;
           var scale = tiles[rand].mesh.bounds.size;
           var adjustPos = tilePos + new Vector3(scale.x / 2,0, -scale.z / 2);
           var tempVase = Instantiate(testSpawn, adjustPos, Quaternion.identity);

           AdjustObjectPlacement(tempVase, 300f);
       }
    }

    private void AdjustObjectPlacement(GameObject theObject, float maxRotation)
    {
        var randRot = Random.Range(0f, maxRotation);
        theObject.transform.Rotate(new Vector3(0,1,0), randRot);

        var objectSize = theObject.GetComponent<MeshFilter>().mesh.bounds.size;
        var randMoveX = Random.Range(0f, objectSize.x / 2);
        var randMoveZ = Random.Range(0f, objectSize.z / 2);

        theObject.transform.position += new Vector3(randMoveX, 0, randMoveZ);
    }


}
