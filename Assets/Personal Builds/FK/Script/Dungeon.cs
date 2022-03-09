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
    [SerializeField] RangesSO rangesSO;
    [SerializeField] UnityEventSO onPlayerPosUpdate;

    // void Start(){
    //     onPlayerPosUpdate.roomEvent.Invoke();
    // }

    void Update(){
        UpdatePlayerPos(playerTransform.position);
    }
    void UpdatePlayerPos(Vector3 playerPosition){
        if (Vector3.Distance(playerPosition, playerTransform.savedPosition) > rangesSO.updatePosThreshold.value){
            Debug.Log("Updating player pos");
            playerTransform.SavePosition();
            onPlayerPosUpdate.roomEvent.Invoke();
        }
    }
    void OnDrawGizmos(){
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(playerTransform.savedPosition,rangesSO.roomSpawnRange.value);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerTransform.savedPosition, rangesSO.roomDespawnRange.value);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(playerTransform.savedPosition, rangesSO.updatePosThreshold.value);
    }
}
