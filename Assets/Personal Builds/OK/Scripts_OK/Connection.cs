using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;


public enum ConnectionType{
    OpenConnection,
    ClosedConnection,
    UsedConnection,
    SuspendedConnection
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
    [SerializeField] DifficultyDependantRoomList activeRoomListSo;
    [Header("Changable Difficulty Rooms")]
    [SerializeField] DifficultyDependantRoomList easyRoomListSo;
    [SerializeField] DifficultyDependantRoomList mediumRoomListSo;
    [SerializeField] DifficultyDependantRoomList hardRoomListSo;
    [SerializeField] DifficultyDependantRoomList nightmareRoomListSo;
    [Header("")]
    [SerializeField] ConnectionType connectionType;
    [SerializeField] GameDifficultySO gameDifficultySo;
    [SerializeField] List<GameObject> environmentToggleList;
    public ConnectionDirection connectionDirection; //TODO: remove public

    public GameObject spawnedRoom;

    List<int> exhaustedNumbers = new List<int>();

    int roomNumber;

    [System.NonSerialized] public UnityEvent becameOpenConnectionEvent;


    public ConnectionType ConnectionType{
        get => connectionType;
        set{
            Debug.Log(value);
            connectionType = value;
            if (value is ConnectionType.OpenConnection){
                //becameOpenConnectionEvent.Invoke();
                DeactivateEnvironmentBlockers();
            }
            else if (value is ConnectionType.ClosedConnection){
                ActivateEnvironmentBlockers();
            }
        }
    }

    //bool validatedRoom;
    int attempt;


    void Awake(){
        //ConnectionType = ConnectionType.UndecidedConnection;
        AssignConnectionDirection();
        if (ConnectionType == ConnectionType.OpenConnection){
            DeactivateEnvironmentBlockers();
        }
        else if (ConnectionType == ConnectionType.ClosedConnection){
            ActivateEnvironmentBlockers();
        }

    }

    void Start(){
        gameDifficultySo.difficultyChangeEvent.AddListener(SetActiveRoomList);
        SetActiveRoomList(gameDifficultySo.Difficulty);
    }

    void OnDisable(){
        gameDifficultySo.difficultyChangeEvent.RemoveListener(SetActiveRoomList);
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


    public Vector3 GetSpawnPosition()
    {
        // var randomRoom = PickRoomToSpawn();
        // var randomRoomRoom = randomRoom.GetComponent<Room>();
        //
        // Vector3 offset = Vector3.zero;
        //
        // foreach (var connection in randomRoomRoom.connections){
        //     if (CheckOppositeDirection(connectionDirection, connection.connectionDirection)){
        //         offset = connection.transform.position;
        //     }
        // }
        //
        // return transform.position - offset;
        return default;
    }

    int GetRandomNumber(int maxInt){
        return Random.Range(0, maxInt);
    }

    GameObject PickRoomToSpawn(){
        //Randomize with seed which room gets picked
        //SpawnRoom();


        roomNumber = GetRandomNumber(activeRoomListSo.combinedPrefabList.Count);
        // while (exhaustedNumbers.Contains(roomNumber) && exhaustedNumbers.Count < activeRoomListSo.combinedPrefabList.Count){
        //     roomNumber = GetRandomNumber(activeRoomListSo.combinedPrefabList.Count);
        // }

        print(roomNumber);
        print("Count:" + activeRoomListSo.combinedPrefabList.Count);
        var room = activeRoomListSo.combinedPrefabList[roomNumber];
        exhaustedNumbers.Add(roomNumber);
        return room;
    }

    [ContextMenu("Spawn Room")]
    public void SpawnRoom(){

        attempt = 0;
        while (ConnectionType is ConnectionType.OpenConnection &&  spawnedRoom == null && attempt < activeRoomListSo.combinedPrefabList.Count){
            var randomPrefabRoom = PickRoomToSpawn();
            var randomRoom = randomPrefabRoom.GetComponent<Room>();

            Vector3 offset = Vector3.zero;

            foreach (var connection in randomRoom.connections){
                if (CheckOppositeDirection(connectionDirection, connection.connectionDirection)){
                    offset = connection.transform.position;
                    //connection.connectionType = ConnectionType.UsedConnection; //TODO: This only affects the prefab, which is really bad. But Needed until we fix Validation.
                }
            }
            attempt++;
            spawnedRoom = Instantiate(randomPrefabRoom,transform.position - offset,quaternion.identity);
            //validatedRoom = true;

            // StartCoroutine(MoveOnTimer(spawnedRoom,transform.position-offset));
            // spawnedRoom.transform.position = transform.position - offset;
            var spawnedRoomRoom = spawnedRoom.GetComponent<Room>();


            if (!spawnedRoomRoom.ValidateRoom()){
                StartCoroutine(DestroyOnTimer(spawnedRoom));
                ConnectionType = ConnectionType.ClosedConnection;
            }

            else{
                foreach (var connection in spawnedRoomRoom.connections){
                    if (CheckOppositeDirection(connectionDirection, connection.connectionDirection)){
                        connection.connectionType = ConnectionType.UsedConnection; //TODO: This only affects the prefab, which is really bad. But Needed until we fix Validation.
                    }
                }
            }

        }
    }

    IEnumerator MoveOnTimer(GameObject room, Vector3 vector3){
        yield return new WaitForSeconds(0.3f);
        room.transform.position = vector3;
    }

    // IEnumerator ValidRoomCheck(Room room){
    //     yield return new WaitForSeconds(0.5f);
    //     if (!room.IsValidRoom){
    //         Destroy(room); //TODO: instead of destroying we want to try the other connections
    //         ConnectionType = ConnectionType.ClosedConnection;
    //         validatedRoom = false;
    //     }
    // }

    IEnumerator DestroyOnTimer(GameObject room){
        yield return new WaitForSeconds(1f);
        Destroy(room);
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


    void DeactivateEnvironmentBlockers(){
        foreach (var _gameObject in environmentToggleList){
            _gameObject.SetActive(false);
        }
    }

    void ActivateEnvironmentBlockers(){
        foreach (var _gameObject in environmentToggleList){
            _gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Changes the active room list depending when difficulty changes
    /// </summary>
    /// <param name="newDifficulty"></param>
    void SetActiveRoomList(Difficulty newDifficulty){

        if (newDifficulty is Difficulty.Easy){
            activeRoomListSo = easyRoomListSo;
        }
        else if (newDifficulty is Difficulty.Medium){
            activeRoomListSo = mediumRoomListSo;
        }
        else if (newDifficulty is Difficulty.Hard){
            activeRoomListSo = hardRoomListSo;
        }
        else if (newDifficulty is Difficulty.Nightmare){
            activeRoomListSo = nightmareRoomListSo;
        }
    }

    [ContextMenu("Connection type check")]
    void Test1(){
        ConnectionType = ConnectionType.ClosedConnection;
    }

    [ContextMenu("Connection type check2")]
    void Test2(){
        ConnectionType = ConnectionType.OpenConnection;
    }


}
