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
       CreatTileSquare(new Vector2(8, 7), transform.position);
    }

    private void CreatTileSquare(Vector2 tileCount, Vector3 startPos)
    {
        for (int i = 0; i <= tileCount.y; i++)
        {
            CreateTileRow((int)tileCount.x, startPos);
            startPos += new Vector3(0, 0, currentTileSize.z);
        }
    }

    private void CreateTileRow(int rowCount, Vector3 startPos)
    {
        for (int i = 0; i < rowCount; i++)
        {
            var testTile = Instantiate(TileTest);
            currentTileSize = testTile.GetComponent<MeshFilter>().mesh.bounds.size;
            testTile.transform.position = startPos + new Vector3(currentTileSize.x, 0);
            startPos = testTile.transform.position;
        }
    }
}
