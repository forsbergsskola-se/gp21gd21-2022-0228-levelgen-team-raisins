using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonTest : MonoBehaviour
{
    private List<GameObject> availableRooms = new List<GameObject>();
    private List<SpawnedRooms> SpawnedRooms = new List<SpawnedRooms>();

    void Start()
    {
        availableRooms = GameObject.FindGameObjectsWithTag("Room").ToList();
        if (availableRooms.Count > 0)
        {
            var aBounds = availableRooms[0].gameObject.GetComponentsInChildren<Collider>()
                .Where(x => x.CompareTag("Bounds")).ToList();

            Debug.Log(aBounds[0].bounds.min);
            Debug.Log(aBounds[0].bounds.max);

            SpawnedRooms.Add(new SpawnedRooms()
            {
                spawnPos = availableRooms[0].transform.position,
                boundsCollider = aBounds[0]
            });
        }

    }

    private void RoomTest()
    {
        for (int i = 0; i < 6; i++)
        {
            var rand = Random.Range(0, availableRooms.Count - 1);
           var bValue = availableRooms[rand].GetComponent<Room>().SingleRoomSpawn(SpawnedRooms);

           Debug.Log(bValue);
            availableRooms = GameObject.FindGameObjectsWithTag("Room").ToList();
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
