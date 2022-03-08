using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using Unity.Multiplayer.Samples.BossRoom;
using Unity.Multiplayer.Samples.BossRoom.Client;
using UnityEngine;
using UnityEngine.AI;


public class Dungeon : MonoBehaviour{
    [SerializeField] PositionSO playerTransform;
    [SerializeField] UnityRoomEventSO roomEventSo;
    [SerializeField] NavMeshSurface navMesh;

    [SerializeField] List<Room> rooms; //Have room fire off an event with their room script as input when validated add
                                       //to this list,and when destroy, remove from list?
    [SerializeField] float roomSpawnRange = 30f;
    [SerializeField] float roomDespawnRange = 60f;
    [SerializeField] float updatePosThreshold = 1f;
    //Vector3 playerOldPosition;

    public List<Room> Rooms{
        get => rooms;
        set{
            rooms = value;
            //GenerateNewRooms();
        }
    }

    void Start(){
        playerTransform.SavePosition();
        roomEventSo.roomEvent.AddListener(AddToActiveRooms);
        GenerateNewRooms();
        navMesh = GetComponent<NavMeshSurface>();
    }


    void AddToActiveRooms(Room room){
        Rooms.Add(room);
        GenerateNewRooms();
    }
    void Update(){
        UpdatePlayerPos(playerTransform.position);
    }
    void UpdatePlayerPos(Vector3 playerPosition){
        if (Vector3.Distance(playerPosition, playerTransform.savedPosition) > updatePosThreshold){
            playerTransform.SavePosition();
            ActivateSuspendedRooms();
            GenerateNewRooms();

        }
    }

    void GenerateNewRooms(){
        print("Generating new rooms");
        foreach (var room in rooms){
            if (Vector3.Distance(room.transform.position, playerTransform.savedPosition) < roomSpawnRange){
                room.SpawnRooms();
            }
        }
        //TODO:navmesh magic here
        // List<Room> newRooms = new List<Room>();
        // foreach (var room in rooms){
        //     if (Vector3.Distance(room.transform.position,playerTransform.position) < roomSpawnRange){
        //         newRooms = room.SpawnRooms();
        //         foreach (var newRoom in newRooms){
        //             rooms.Add(newRoom);
        //         } //TODO: When we spawned the first set of rooms we need to check if we should spawn more
        //
        //         //here we add the new room to rooms list
        //     }
        // }
    }

    void ActivateSuspendedRooms(){
        foreach (var room in rooms){
            foreach (var connection in room.connections){
                if (connection.ConnectionType is ConnectionType.SuspendedConnection){
                    connection.ConnectionType = ConnectionType.OpenConnection;
                }

            }
        }
    }





    void DisableRoomConnections(){
        //If outside permitted spawn range, but inside existance range, turn all connections to ConnectionType.ClosedConnection;
        //To prevent more rooms from spawning
    }

    void ReturnRoomsToPool(){
        //If outside existance range, return to pool.
    }




    void OnDrawGizmos(){
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(playerTransform.savedPosition,roomSpawnRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerTransform.savedPosition, roomDespawnRange);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(playerTransform.savedPosition, updatePosThreshold);
    }
}
