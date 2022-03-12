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

    void Update(){
        if (!PlayerIsWithinUpdateRange()){
            return;
        }

        UpdatePlayerPos();
    }

    void UpdatePlayerPos(){
        playerTransform.SavePosition();
        onPlayerPosUpdate.roomEvent.Invoke();
    }

    bool PlayerIsWithinUpdateRange(){
        if (Vector3.Distance(playerTransform.position, playerTransform.savedPosition) >
            rangesSO.updatePosThreshold.value){
            return true;
        }

        return false;
    }

    void OnDrawGizmos(){
        DrawColoredGizmoWireSphere(playerTransform.savedPosition, rangesSO.roomSpawnRange.value, Color.green);
        DrawColoredGizmoWireSphere(playerTransform.savedPosition, rangesSO.roomDespawnRange.value, Color.red);
        DrawColoredGizmoWireSphere(playerTransform.savedPosition, rangesSO.updatePosThreshold.value, Color.magenta);
    }

    void DrawColoredGizmoWireSphere(Vector3 origin, float range, Color color){
        Gizmos.color = color;
        Gizmos.DrawWireSphere(origin, range);
        Gizmos.color = Color.white;
    }
}
