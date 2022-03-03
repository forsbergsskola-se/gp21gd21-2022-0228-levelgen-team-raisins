using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public enum ConnectionType{
    OpenConnection,
    ClosedConnection,
    UndecidedConnection
}

public class Connection : MonoBehaviour{
    [SerializeField] PrefabListSO prefabListSo;


    bool validatedRoom;
    int attempt;



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
            attempt++;
            //random room
            //instatiate room
            Instantiate(randomRoom,transform.position,quaternion.identity);
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
