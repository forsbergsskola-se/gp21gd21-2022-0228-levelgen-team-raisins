using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour{
    [SerializeField] public List<Connection> connections;

    [SerializeField] PositionSO playerTransform;
    [SerializeField] RangesSO rangesSO;
    [SerializeField] UnityEventSO onPlayerPosUpdate;
    [SerializeField] GameEventSO onRoomSpawned;

    public Connection spawnedConnection;

    static int id;

    void Awake(){
        gameObject.name = $"room {id++}";
    }

    void Start(){
        onPlayerPosUpdate.roomEvent.AddListener(DestroyRooms);
        onPlayerPosUpdate.roomEvent.AddListener(SpawnRooms);

        if (onRoomSpawned != null){
            onRoomSpawned.Invoke();
        }
    }

    void OnDisable(){
        onPlayerPosUpdate.roomEvent.RemoveListener(DestroyRooms);
        onPlayerPosUpdate.roomEvent.RemoveListener(SpawnRooms);
    }

    void SpawnRooms(){
        if (!RoomIsWithinSpawnRangeCheck()){
            return;
        }

        foreach (var connection in connections){
            if (connection == null){
                continue;
            }

            connection.SpawnRoom();
        }
    }

    void DestroyRooms(){
        if (!RoomIsOutsideDespawnRangeCheck()){
            return;
        }

        if (spawnedConnection != null){
            ChangeConnectionType(spawnedConnection, ConnectionType.OpenConnection);
        }

        Destroy(this.gameObject);
    }

    bool RoomIsWithinSpawnRangeCheck(){
        if (Vector3.Distance(this.transform.position, playerTransform.savedPosition) < rangesSO.roomDespawnRange.value){
            return true;
        }

        return false;
    }
    bool RoomIsOutsideDespawnRangeCheck(){
        if (Vector3.Distance(this.transform.position, playerTransform.savedPosition) >= rangesSO.roomDespawnRange.value){
            return true;
        }

        return false;
    }
    void ChangeConnectionType(Connection connection, ConnectionType newConnectionType){
        connection.ConnectionType = newConnectionType;
    }
}
