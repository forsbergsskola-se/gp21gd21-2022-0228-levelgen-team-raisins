using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_ML : MonoBehaviour
{

    [SerializeField] private GameObject TileTest;
    private List<Vector3> tilePos = new List<Vector3>();

    private Vector3 GetMeshSize(GameObject theObject)
    {
        return theObject.GetComponent<MeshFilter>().mesh.bounds.size;
    }

    void Start()
    {
       CreateTileRow();
    }

    private void CreateTileRow()
    {
        var testTile = Instantiate(TileTest);
        var size = testTile.GetComponent<MeshFilter>().mesh.bounds.size.x;
        var currentPos = testTile.transform.position;

        for (int i = 0; i < 8; i++)
        {
            testTile = Instantiate(TileTest);
            testTile.transform.position += currentPos + new Vector3(size, 0);
            currentPos = testTile.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
