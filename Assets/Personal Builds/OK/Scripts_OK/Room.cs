using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;


public enum RoomType{
    LargeRoom,
    MediumRoom,
    SmallRoom,
    Corridor,

}

public class Room : MonoBehaviour{
    [SerializeField] public List<Connection> connections; //Reference door scripts
    public List<RoomValidator> RoomValidators;

    bool isValidRoom = true;

    public bool IsValidRoom{
        get => isValidRoom;
        set{
            isValidRoom = value;
        }
    }


    public bool HasFreeConnections()
    {
        return connections.Any(connection => connection.ConnectionType == ConnectionType.OpenConnection);
    }

    public bool SingleRoomSpawn(List<SpawnedRooms> SpawnedRooms)
    {
        var  temp = connections.FirstOrDefault(connection => connection.ConnectionType == ConnectionType.OpenConnection);

        if (temp == default) return false;

        var spawnPosition = temp.GetSpawnPosition();

        var condition = SpawnedRooms.Where(spawnedRoom =>
        {
            if (Vector3.Distance(spawnPosition, spawnedRoom.spawnPos) < 10) //Magic number
            {
                return true;
            }

            return false;
        }).ToList().Count;

        if(condition > 0) return false;

        temp.SpawnRoom();
        temp.ConnectionType = ConnectionType.ClosedConnection;
        SpawnedRooms.Add(new SpawnedRooms()
        {
            spawnPos =  spawnPosition
        });
        return true;
    }

    public void RuntimeSpawn()
    {
        var temp = connections
            .Where(connection => connection.ConnectionType == ConnectionType.OpenConnection)
            .Select(connection =>
            {
                connection.SpawnRoom();
                connection.ConnectionType = ConnectionType.ClosedConnection;
                return connection;
            } ).ToList();
    }

     public int activeConnections;

    bool isCompletedRoom;
    public bool IsCompletedRoom{
        get => isCompletedRoom;
        set{
            isCompletedRoom = value;
            if (isCompletedRoom == true){
                SpawnRooms();
            }
        }
    }
//bug
 //   void OnEnable()
 //   {
 //       foreach (var roomValidator in RoomValidators){
 //           roomValidator.RoomValidationEvent.AddListener(ValidateRoom);
 //       }
 //       activeConnections = 0;
 //       foreach (var connection in connections){
 //           connection.becameOpenConnectionEvent.AddListener(AddActiveConnections);
 //       }
 //   }
 //   void OnDisable(){
 //       foreach (var roomValidator in RoomValidators){
 //           roomValidator.RoomValidationEvent.RemoveListener(ValidateRoom);
 //       }
 //       foreach (var connection in connections){
 //           connection.becameOpenConnectionEvent.RemoveListener(ReduceActiveConnections);
 //       }
 //   }


    // void AddActiveConnections(){
    //     activeConnections++;
    // }
    // void ReduceActiveConnections(){
    //     activeConnections--;
    // }

    public void ValidateRoom(bool value){
        isValidRoom = value;
        //if boids boxcast tell us the room is spawned outside the old room its allowed to spawn
    }

    [ContextMenu("Spawn All Available Rooms")]
    public void SpawnRooms(){
        foreach (var connection in connections){

            if (connection.ConnectionType is ConnectionType.OpenConnection){
                connection.SpawnRoom();
            }

        }
    }
}
