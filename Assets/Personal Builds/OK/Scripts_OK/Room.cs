using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;


public enum RoomType{
    LargeRoom,
    MediumRoom,
    SmallRoom,
    Corridor,

}

public class Room : MonoBehaviour{
    [SerializeField] public List<Connection> connections; //Reference door scripts
    public UnityRoomEventSO roomEventSo;
    public List<RoomValidator> roomValidators;



    bool isValidRoom = true;

    public bool IsValidRoom{
        get => isValidRoom;
        set{
            isValidRoom = value;
            if (value){
                roomEventSo.roomEvent.Invoke(this);
                SpawnInternals();
            }

            if (!value){
                Destroy(this.gameObject);
            }
        }
    }

    void Start(){
        //roomEventSo.roomEvent.Invoke(this);
    }

    public bool HasFreeConnections()
    {
        return connections.Any(connection => connection.ConnectionType == ConnectionType.OpenConnection);
    }

    public bool SingleRoomSpawn(List<SpawnedRooms> SpawnedRooms)
    {
        var  temp = connections.FirstOrDefault(connection => connection.ConnectionType == ConnectionType.OpenConnection);

        if (temp == default) return false;

        var spawnPosition = temp.GetSpawnPosition();

        var condition = SpawnedRooms.Where(spawnedRoom =>
        {
            if (Vector3.Distance(spawnPosition, spawnedRoom.spawnPos) < 10) //Magic number
            {
                return true;
            }

            return false;
        }).ToList().Count;

        if(condition > 0) return false;

        temp.SpawnRoom();
        temp.ConnectionType = ConnectionType.ClosedConnection;
        SpawnedRooms.Add(new SpawnedRooms()
        {
            spawnPos =  spawnPosition
        });
        return true;
    }

    public void RuntimeSpawn()
    {
        var temp = connections
            .Where(connection => connection.ConnectionType == ConnectionType.OpenConnection)
            .Select(connection =>
            {
                connection.SpawnRoom();
                connection.ConnectionType = ConnectionType.ClosedConnection;
                return connection;
            } ).ToList();
    }

     public int activeConnections;

    bool isCompletedRoom;
    public bool IsCompletedRoom{
        get => isCompletedRoom;
        set{
            isCompletedRoom = value;
            if (isCompletedRoom == true){
                SpawnRooms();
            }
        }
    }
//bug
 //   void OnEnable()
 //   {
 //       foreach (var roomValidator in RoomValidators){
 //           roomValidator.RoomValidationEvent.AddListener(ValidateRoom);
 //       }
 //       activeConnections = 0;
 //       foreach (var connection in connections){
 //           connection.becameOpenConnectionEvent.AddListener(AddActiveConnections);
 //       }
 //   }
 //   void OnDisable(){
 //       foreach (var roomValidator in RoomValidators){
 //           roomValidator.RoomValidationEvent.RemoveListener(ValidateRoom);
 //       }
 //       foreach (var connection in connections){
 //           connection.becameOpenConnectionEvent.RemoveListener(ReduceActiveConnections);
 //       }
 //   }4


 public bool ValidateRoom(){

     foreach (var roomValidator in roomValidators){
         if (roomValidator.isColliding){
             IsValidRoom = false;
             return IsValidRoom;
         }
     }

     return IsValidRoom;

 }



    [ContextMenu("Spawn All Available Rooms")]
    public void SpawnRooms(){
        if (ValidateRoom()){
            StartCoroutine(nameof(SpawnRoomsOnTimer));
        }

    }

    IEnumerator SpawnRoomsOnTimer(){
        //List<Room> rooms = new List<Room>();
        foreach (var connection in connections){

            if (connection.ConnectionType is ConnectionType.OpenConnection){
                var room = connection.SpawnRoom();
                //connection.ConnectionType = ConnectionType.UsedConnection;
                //rooms.Add(room);
                yield return new WaitForSeconds(0.4f);
            }
        }
       // yield return rooms;
       yield return null;
    }

    public void SpawnInternals(){
        //This should spawn all items inside the room.
        //The items need to be picked at random from a list of possible items


      //-take into account rooms which allow both easy and medium enemies to spawn
      //Foreach prefablistSO in ...something?
            //foreach gameobject in prefablistSO
    }
}
