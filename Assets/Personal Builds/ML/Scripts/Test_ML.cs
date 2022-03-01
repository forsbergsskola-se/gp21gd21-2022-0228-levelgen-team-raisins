using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_ML : MonoBehaviour
{

    [SerializeField] private GameObject TileTest;
    private List<Vector3> tilePos = new List<Vector3>();
    private Vector3 currentTileSize;
    private Vector3 currentPosition;

    private Vector3 GetMeshSize(GameObject theObject)
    {
        return theObject.GetComponent<MeshFilter>().mesh.bounds.size;
    }

    void Start()
    {
       CreatTileSquare(new Vector2(8, 7));
    }

    private void CreatTileSquare(Vector2 tileCount)
    {
        var startVector = transform.position;
        currentPosition = startVector;
        for (int i = 0; i < tileCount.y; i++)
        {
            CreateTileRow((int)tileCount.x);
            currentPosition = startVector
                              + new Vector3(0, 0, currentTileSize.z * i);
        }
    }

    private void CreateTileRow(int rowCount)
    {
        for (int i = 0; i < rowCount; i++)
        {
            var testTile = Instantiate(TileTest);
            currentTileSize = testTile.GetComponent<MeshFilter>().mesh.bounds.size;
            testTile.transform.position = currentPosition + new Vector3(currentTileSize.x, 0);
            currentPosition = testTile.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
