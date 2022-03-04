using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;


public enum ConnectionType{
    OpenConnection,
    ClosedConnection,
    UndecidedConnection
}

public enum ConnectionDirection{
    Unassigned,
    Up,
    Down,
    Left,
    Right
}

[ExecuteInEditMode]//TODO:REMOVE used for debug
public class Connection : MonoBehaviour{
    [SerializeField] PrefabListSO prefabListSo;
    [SerializeField] ConnectionType connectionType;
    public ConnectionDirection connectionDirection; //TODO: remove public


    [System.NonSerialized] public UnityEvent becameOpenConnectionEvent;

    public ConnectionType ConnectionType{
        get => connectionType;
        set{
            connectionType = value;
            if (connectionType == ConnectionType.OpenConnection){
                becameOpenConnectionEvent.Invoke();
            }
        }
    }

    // public ConnectionDirection ConnectionDirection{
    //     get => connectionDirection;
    //     set{
    //         connectionDirection = value;
    //     }
    // }

    bool validatedRoom;
    int attempt;


    void Awake(){
        //ConnectionType = ConnectionType.UndecidedConnection;
        AssignConnectionDirection();
    }

    void AssignConnectionDirection(){
        if (this.transform.localPosition.z > Vector3.forward.z){
            connectionDirection = ConnectionDirection.Up;
        }
        if (this.transform.localPosition.z < Vector3.back.z){
            connectionDirection = ConnectionDirection.Down;
        }
        if (this.transform.localPosition.x < Vector3.left.x){
            connectionDirection = ConnectionDirection.Left;
        }
        if (this.transform.localPosition.x > Vector3.right.x){
            connectionDirection = ConnectionDirection.Right;
        }
    }


    GameObject PickRoomToSpawn(){
        //Randomize with seed which room gets picked
        //SpawnRoom();
        var room = prefabListSo.prefabs[0]; //For testing purposes
        return room;
    }

    [ContextMenu("Spawn Room")]
    public void SpawnRoom(){

        attempt = 0;
        while (validatedRoom == false && attempt < prefabListSo.prefabs.Count){
            var randomRoom = PickRoomToSpawn();
            var randomRoomRoom = randomRoom.GetComponent<Room>();

            Vector3 offset = Vector3.zero;

            foreach (var connection in randomRoomRoom.connections){
                if (CheckOppositeDirection(connectionDirection, connection.connectionDirection)){
                    offset = connection.transform.position;
                }

            }

            // if (connectionDirection == ConnectionDirection.Up){
            //     randomRoomRoom.connections
            // }

           // var offset =  randomRoomRoom.connections[0].transform.position;

            //var offset = Vector3.Distance(randomRoomRoom.connections[0].transform.position, transform.position); //Testing purposes
           // var offsetVector = new Vector3(offset, offset, offset);
            attempt++;
            //random room
            //instatiate room
            var spawnedRoom = Instantiate(randomRoom,transform.position - offset,quaternion.identity);
            Debug.Log(transform.name +$": Pos {transform.position}, Offset {offset}");
            // if (ValidateRoom()){
            //     break;
            // }
        }

    }


    bool CheckOppositeDirection(ConnectionDirection direction1,ConnectionDirection direction2){
        if (direction1 is ConnectionDirection.Up && direction2 is ConnectionDirection.Down) return true;
        else if (direction1 is ConnectionDirection.Down && direction2 is ConnectionDirection.Up) return true;
        else if (direction1 is ConnectionDirection.Left && direction2 is ConnectionDirection.Right) return true;
        else if (direction1 is ConnectionDirection.Right && direction2 is ConnectionDirection.Left) return true;
        else return false;
    }

    //If validate fails, open up new connection

    void ValidateRoom(GameObject prefab){
        //Check if room overlaps


        //if successful, set validatedRoom = true;
    }

}
