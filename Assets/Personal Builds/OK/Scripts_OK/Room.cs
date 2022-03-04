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


    public void RuntimeSpawn()
    {
        var temp = connections
            .Where(x => x.ConnectionType == ConnectionType.OpenConnection)
            .Select(x =>
            {
                x.SpawnRoom();
                return x;
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


    void AddActiveConnections(){
        activeConnections++;
    }
    void ReduceActiveConnections(){
        activeConnections--;
    }

    public void ValidateRoom(bool value){
        isValidRoom = value;
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
