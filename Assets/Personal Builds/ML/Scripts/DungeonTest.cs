using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum SpawnDirection
{
    XPlus, XMinus, ZPlus, ZMinus
}

public class DungeonTest : MonoBehaviour
{
    private List<GameObject> availableRooms = new List<GameObject>();
    private List<SpawnedRooms> SpawnedRooms = new List<SpawnedRooms>();
    public GameObject spawnRoom;

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
        FindNextSpawnPoint(SpawnedRooms[0].roomColliders[0].bounds, SpawnDirection.ZPlus);
    }


    private void FindNextSpawnPoint(Bounds roomBounds, SpawnDirection spawnDirection)
    {
        var x = roomBounds.center.x;
        var y = 0f;
        var z = roomBounds.center.z;

        switch (spawnDirection)
        {
            case SpawnDirection.XMinus:
                x = roomBounds.min.x - roomBounds.extents.x;
                break;
            case SpawnDirection.XPlus:
                x = roomBounds.max.x + roomBounds.extents.x;
                break;
            case SpawnDirection.ZMinus:
                z = roomBounds.min.z - roomBounds.extents.z;
                break;
            case SpawnDirection.ZPlus:
                z = roomBounds.max.z + roomBounds.extents.z;
                break;
        }

        Instantiate(spawnRoom, new Vector3(x, y, z), Quaternion.identity);
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
