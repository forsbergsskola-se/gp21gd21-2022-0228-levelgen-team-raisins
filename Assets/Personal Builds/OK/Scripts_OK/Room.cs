using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum RoomType{
    LargeRoom,
    MediumRoom,
    SmallRoom,
    Corridor,

}

public class Room : MonoBehaviour{
    [SerializeField] public List<Connection> connections; //Reference door scripts

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

    void OnEnable(){
        activeConnections = 0;
        foreach (var connection in connections){
            connection.becameOpenConnectionEvent.AddListener(AddActiveConnections);
        }
    }
    void OnDisable(){
        foreach (var connection in connections){
            connection.becameOpenConnectionEvent.RemoveListener(ReduceActiveConnections);
        }
    }


    void AddActiveConnections(){
        activeConnections++;
    }
    void ReduceActiveConnections(){
        activeConnections--;
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
