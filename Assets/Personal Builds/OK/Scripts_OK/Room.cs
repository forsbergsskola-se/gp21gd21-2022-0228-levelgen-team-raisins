using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.Rendering;


public enum RoomType{
    LargeRoom,
    MediumRoom,
    SmallRoom,
    Corridor,
}

public class Room : MonoBehaviour{
    [SerializeField] PositionSO playerTransform;
    [SerializeField] public List<Connection> connections; //Reference door scripts
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
        onRoomSpawned?.Invoke();
    }

    void OnDisable(){
       onPlayerPosUpdate.roomEvent.RemoveListener(DestroyRooms);
        onPlayerPosUpdate.roomEvent.RemoveListener(SpawnRooms);
    }

    void SpawnRooms(){
        StartCoroutine(nameof(CoroutineSpawnRooms));
    }

    void DestroyRooms(){
        if (Vector3.Distance(this.transform.position,playerTransform.savedPosition ) > rangesSO.roomDespawnRange.value){
            //Set spawn connection to Open
            spawnedConnection.ConnectionType = ConnectionType.OpenConnection;
            Destroy(this.gameObject);
        }
    }

   // void


    [ContextMenu("Spawn All Available Rooms")]
    IEnumerator CoroutineSpawnRooms(){
        Debug.Log(name + "Spawning rooms");


        if (Vector3.Distance(this.transform.position,playerTransform.savedPosition ) < rangesSO.roomSpawnRange.value){
            foreach (var connection in connections){
                if (connection == null){
                    continue;
                }
                connection.SpawnRoom();
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
