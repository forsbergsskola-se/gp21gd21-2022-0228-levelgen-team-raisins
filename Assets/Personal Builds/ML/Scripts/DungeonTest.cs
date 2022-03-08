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
            var aBounds = GetRoomBounds(availableRooms[0].gameObject);

            SpawnedRooms.Add(new SpawnedRooms()
            {
                spawnPos = availableRooms[0].transform.position,
                roomColliders = aBounds
            });
        }
        SpawnNewRoom(SpawnedRooms[0].roomColliders);
    }


    private List<Collider> GetRoomBounds(GameObject theRoom)
    {
        var aBounds = theRoom.GetComponentsInChildren<Collider>()
            .Where(x => x.CompareTag("Bounds")).ToList();

        return aBounds;
    }

    private void SpawnNewRoom(List<Collider> oldRoomBounds)
    {

        var nextRoom = Instantiate(spawnRoom, new Vector3(), Quaternion.identity);
        var newRoomBounds = GetRoomBounds(nextRoom);

        var nextSpawnPoint = FindNextSpawnPoint(oldRoomBounds[0].bounds,
            newRoomBounds[0].bounds,SpawnDirection.XMinus);

        nextRoom.transform.position = nextSpawnPoint;

        availableRooms.Add(nextRoom);

        SpawnedRooms.Add(new SpawnedRooms()
        {
            spawnPos = nextSpawnPoint,
            roomColliders = newRoomBounds
        });
    }


    private Vector3 FindNextSpawnPoint(Bounds oldRoomBounds, Bounds newRoomBounds, SpawnDirection spawnDirection)
    {
        var x = oldRoomBounds.center.x;
        var y = 0f;
        var z = oldRoomBounds.center.z;

        switch (spawnDirection)
        {
            case SpawnDirection.XMinus:
                x = oldRoomBounds.min.x - (oldRoomBounds.extents.x / 2 + newRoomBounds.extents.x / 2);
                break;
            case SpawnDirection.XPlus:
                x = oldRoomBounds.max.x + (oldRoomBounds.extents.x / 2 + newRoomBounds.extents.x / 2);
                break;
            case SpawnDirection.ZMinus:
                z = oldRoomBounds.min.z - (oldRoomBounds.extents.z / 2 + newRoomBounds.extents.z / 2);
                break;
            case SpawnDirection.ZPlus:
                z = oldRoomBounds.max.z + (oldRoomBounds.extents.x / 2 + newRoomBounds.extents.x / 2);
                break;
        }

         var outVector = new Vector3(x, y, z);

        return outVector;
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
