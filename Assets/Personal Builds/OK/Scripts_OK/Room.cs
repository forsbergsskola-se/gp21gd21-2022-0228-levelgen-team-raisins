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
    // public UnityRoomEventSO roomEventSo;
    public List<RoomValidator> roomValidators;
    static int id;

    //bool eventHasBeenInvoked;


    [SerializeField] bool isValidRoom = false;

    public bool IsValidRoom{
        get => isValidRoom;
        set{
            isValidRoom = value;
            if (isValidRoom){
                Debug.Log(name +": Is valid");
                SpawnRooms();
                SpawnInternals();
            }

            if (!isValidRoom){
                Destroy(this.gameObject);
            }
        }
    }

    void Awake(){
        gameObject.name = $"room {id++}";

    }

    void Start(){
        onPlayerPosUpdate.roomEvent.AddListener(SpawnRooms);
        this.GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    void OnDisable(){
        onPlayerPosUpdate.roomEvent.RemoveListener(SpawnRooms);
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

        // if (isValidRoom && !eventHasBeenInvoked){
        //     eventHasBeenInvoked = true;
        // }

        return IsValidRoom;
    }


    [ContextMenu("Spawn All Available Rooms")]
    public void SpawnRooms(){
        Debug.Log(name + "Spawning rooms");
        if (Vector3.Distance(this.transform.position,playerTransform.savedPosition ) < rangesSO.roomSpawnRange.value){
            foreach (var connection in connections){
                connection.SpawnRoom();
            }
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
