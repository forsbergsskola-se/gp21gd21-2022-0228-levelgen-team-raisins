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

    static int id;
    void Awake(){
        gameObject.name = $"room {id++}";

    }

    void Start(){
        this.GetComponent<NavMeshSurface>().BuildNavMesh();
        onPlayerPosUpdate.roomEvent.AddListener(SpawnRooms);
    }

    void OnDisable(){
        onPlayerPosUpdate.roomEvent.RemoveListener(SpawnRooms);
    }

    void SpawnRooms(){
        StartCoroutine(nameof(CoroutineSpawnRooms));
        onPlayerPosUpdate.roomEvent.RemoveListener(SpawnRooms);
    }


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
            SpawnInternals();
        }
    }
    public void SpawnInternals(){
        //This should spawn all items inside the room.
        //The items need to be picked at random from a list of possible items


        //-take into account rooms which allow both easy and medium enemies to spawn
        //Foreach prefablistSO in ...something?
        //foreach gameobject in prefablistSO
    }
}
