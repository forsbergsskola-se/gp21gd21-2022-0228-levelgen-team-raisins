using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_ML : MonoBehaviour
{

    [SerializeField] private GameObject TileTest;
    private List<Vector3> tilePos = new List<Vector3>();

    private void GetMeshSize()
    {

    }

    void Start()
    {
       var testTile = Instantiate(TileTest);
       Debug.Log(testTile.GetComponent<Mesh>().bounds.size);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
