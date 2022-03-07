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

            SpawnedRooms.Add(new SpawnedRooms()
            {
                spawnPos = availableRooms[0].transform.position,
                roomColliders = aBounds
            });
        }
        BoundsTest();
    }


    private void BoundsTest()
    {
        Debug.Log(SpawnedRooms[0].roomColliders[0].bounds.extents);
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

    public static bool IsInside(Collider collider, Vector3 point)
    {
        Vector3 closestCollider = collider.ClosestPoint(point);
        return closestCollider == point;
    }

    // Update is called once per frame
    void Update()
    {

    }


}
