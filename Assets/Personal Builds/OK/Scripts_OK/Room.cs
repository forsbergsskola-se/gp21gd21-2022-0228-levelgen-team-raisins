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
    [SerializeField] float roomSpawnDistance = 40;
    public UnityRoomEventSO roomEventSo;
    public List<RoomValidator> roomValidators;
    static int id;

    bool eventHasBeenInvoked;


    bool isValidRoom = true;

    public bool IsValidRoom{
        get => isValidRoom;
        set{
            isValidRoom = value;
            if (isValidRoom){
                SpawnInternals();
            }

            if (!isValidRoom){
                Destroy(this.gameObject);
            }
        }
    }

    void Awake(){
        gameObject.name = $"room {id++}";
        this.GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    void Start(){
        SpawnRooms();
    }
    

    bool isCompletedRoom;

    public bool ValidateRoom(){
        foreach (var roomValidator in roomValidators){
            if (roomValidator.isColliding){
                Debug.Log("is colliding");
                IsValidRoom = false;
                return IsValidRoom;
            }
        }

        if (isValidRoom && !eventHasBeenInvoked){
            eventHasBeenInvoked = true;
        }

        return IsValidRoom;
    }


    [ContextMenu("Spawn All Available Rooms")]
    public void SpawnRooms(){
        if (Vector3.Distance(this.transform.position,playerTransform.savedPosition ) < roomSpawnDistance){
            foreach (var connection in connections){
                connection.SpawnRoom();
            }
        }
    }

    // IEnumerator SpawnRoomsOnTimer(){
    //     Debug.Log(name + "has spawned");
    //     foreach (var connection in connections){
    //         if (connection.ConnectionType is ConnectionType.OpenConnection){
    //             connection.SpawnRoom();
    //             yield return new WaitForSeconds(0.4f);
    //         }
    //     }
    //
    //     roomEventSo.roomEvent.Invoke(this);
    //     yield return null;
    // }

    public void SpawnInternals(){
        //This should spawn all items inside the room.
        //The items need to be picked at random from a list of possible items


        //-take into account rooms which allow both easy and medium enemies to spawn
        //Foreach prefablistSO in ...something?
        //foreach gameobject in prefablistSO
    }
}
