using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;


public enum ConnectionType{
    OpenConnection,
    ClosedConnection,
    UndecidedConnection
}

public class Connection : MonoBehaviour{
    [SerializeField] PrefabListSO prefabListSo;
    [SerializeField] ConnectionType connectionType;


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

    bool validatedRoom;
    int attempt;

    void Awake(){
        ConnectionType = ConnectionType.UndecidedConnection;

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

            var offset =  randomRoomRoom.connections[0].transform.position;

            //var offset = Vector3.Distance(randomRoomRoom.connections[0].transform.position, transform.position); //Testing purposes
           // var offsetVector = new Vector3(offset, offset, offset);
            attempt++;
            //random room
            //instatiate room
            var spawnedRoom = Instantiate(randomRoom,transform.position + offset,quaternion.identity);
            Debug.Log(transform.name +$": Pos {transform.position}, Offset {offset}");
            // if (ValidateRoom()){
            //     break;
            // }
        }

    }

    //If validate fails, open up new connection

    void ValidateRoom(GameObject prefab){
        //Check if room overlaps


        //if successful, set validatedRoom = true;
    }

}
