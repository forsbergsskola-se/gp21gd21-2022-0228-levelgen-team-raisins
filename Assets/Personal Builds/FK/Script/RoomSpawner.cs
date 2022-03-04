using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using Unity.Multiplayer.Samples.BossRoom;
using Unity.Multiplayer.Samples.BossRoom.Client;
using UnityEngine;


public class RoomSpawner : MonoBehaviour{
    [SerializeField] Transform playerTransform;
    [SerializeField] List<Room> rooms;
    float roomSpawnRadius = 60f;
    float updatePosThreashold = 1f;
    Transform PlayerOldTransform;

    void Start(){
        PlayerOldTransform.position = Vector3.zero;
        playerTransform = FindObjectOfType<PlayerTag>().transform;
    }

    void Update(){
        UpdatePlayerPos(playerTransform);
    }
    void UpdatePlayerPos(Transform player){
        if (Vector3.Distance(player.transform.position, PlayerOldTransform.position) > updatePosThreashold){
            GenerateNewRooms();
        }
    }

    void GenerateNewRooms(){
        foreach (var room in rooms){
            if (Vector3.Distance(room.transform.position,playerTransform.position) < roomSpawnRadius){
                room.SpawnRooms();
            }
        }
    }




    // void OnDrawGizmos(){
    //     Gizmos.DrawWireSphere(playerTransform.position,updatePosThreashold);
    // }
}
