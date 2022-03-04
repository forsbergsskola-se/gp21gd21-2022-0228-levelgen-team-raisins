using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonTest : MonoBehaviour
{
    private List<GameObject> availableRooms = new List<GameObject>();

    void Start()
    {
        availableRooms = GameObject.FindGameObjectsWithTag("Room").ToList();
        availableRooms[0].GetComponent<Room>().connections[0].SpawnRoom();
    }

    private void RoomTest()
    {
        availableRooms[0].GetComponent<Room>().connections[0].SpawnRoom();
        var conn = availableRooms[0].GetComponent<Room>().connections;

        Debug.Log("something");
        foreach (var c in conn)
        {
            Debug.Log(c.ConnectionType);
        }

    }

    public static bool IsInside(Collider c, Vector3 point)
    {
        Vector3 closest = c.ClosestPoint(point);
        // Because closest=point if point is inside - not clear from docs I feel
        return closest == point;
    }

    // Update is called once per frame
    void Update()
    {

    }


}
