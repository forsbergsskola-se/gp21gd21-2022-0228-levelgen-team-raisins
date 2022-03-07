using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using Unity.Multiplayer.Samples.BossRoom;
using Unity.Multiplayer.Samples.BossRoom.Client;
using UnityEngine;


public class Dungeon : MonoBehaviour{
    [SerializeField] PositionSO playerTransform;
    [SerializeField] RoomListSO activeRooms; //Have room fire off an event with their room script as input when validated add
                                       //to this list,and when destroy, remove from list?
    [SerializeField] float roomSpawnRadius = 60f;
    [SerializeField] float updatePosThreshold = 1f;
    Vector3 playerOldPosition;

    void Start(){
        playerOldPosition = Vector3.zero;
    }

    void Update(){
        UpdatePlayerPos(playerTransform.position);
    }
    void UpdatePlayerPos(Vector3 playerPosition){
        if (Vector3.Distance(playerPosition, playerOldPosition) > updatePosThreshold){
            GenerateNewRooms();
        }
    }

    void GenerateNewRooms(){
        foreach (var room in activeRooms.rooms){
            if (Vector3.Distance(room.transform.position,playerTransform.position) < roomSpawnRadius){
                room.SpawnRooms(); //TODO: Here we want to return the room so we can add it to the list
                //here we add the new room to rooms list
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




    // void OnDrawGizmos(){
    //     Gizmos.DrawWireSphere(playerTransform.position,updatePosThreashold);
    // }
}
