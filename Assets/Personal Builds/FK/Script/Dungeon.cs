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

    [SerializeField] float roomSpawnRange = 30f;
    [SerializeField] float roomDespawnRange = 60f;
    [SerializeField] float updatePosThreshold = 1f;

    void Update(){
        UpdatePlayerPos(playerTransform.position);
    }
    void UpdatePlayerPos(Vector3 playerPosition){
        if (Vector3.Distance(playerPosition, playerTransform.savedPosition) > updatePosThreshold){
            Debug.Log("updating player pos");
            playerTransform.SavePosition();
        }
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
