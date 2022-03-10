using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if (onRoomSpawned != null){
            onRoomSpawned.Invoke();
        }
    }

    void OnDisable(){
        onPlayerPosUpdate.roomEvent.RemoveListener(DestroyRooms);
        onPlayerPosUpdate.roomEvent.RemoveListener(SpawnRooms);
    }

    void SpawnRooms(){
        //StartCoroutine(nameof(CoroutineSpawnRooms));
        Debug.Log(name + "Spawning rooms");


        if (Vector3.Distance(this.transform.position,playerTransform.savedPosition ) < rangesSO.roomSpawnRange.value){
            foreach (var connection in connections){
                if (connection == null){
                    continue;
                }
                connection.SpawnRoom();
                //yield return new WaitForSeconds(0.3f);
            }
        }
    }

    void DestroyRooms(){
        if (Vector3.Distance(this.transform.position,playerTransform.savedPosition ) >= rangesSO.roomDespawnRange.value){
            //Set spawn connection to Open
            if (spawnedConnection != null){
               spawnedConnection.ConnectionType = ConnectionType.OpenConnection;
            }

            Destroy(this.gameObject);
        }
    }

   // [ContextMenu("Spawn All Available Rooms")]
    // IEnumerator CoroutineSpawnRooms(){
    //     Debug.Log(name + "Spawning rooms");
    //
    //
    //     if (Vector3.Distance(this.transform.position,playerTransform.savedPosition ) < rangesSO.roomSpawnRange.value){
    //         foreach (var connection in connections){
    //             if (connection == null){
    //                 continue;
    //             }
    //             connection.SpawnRoom();
    //             //yield return new WaitForSeconds(0.3f);
    //         }
    //     }
    //
    //     yield return default;
    // }
}
