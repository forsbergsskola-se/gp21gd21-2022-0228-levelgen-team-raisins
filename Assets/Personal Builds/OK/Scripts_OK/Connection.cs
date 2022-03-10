using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
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

[ExecuteInEditMode] //TODO:REMOVE used for debug
public class Connection : MonoBehaviour{
    [SerializeField] DifficultyDependantRoomList activeRoomListSo;

    [Header("Changable Difficulty Rooms")] [SerializeField]
    DifficultyDependantRoomList easyRoomListSo;

    [SerializeField] DifficultyDependantRoomList mediumRoomListSo;
    [SerializeField] DifficultyDependantRoomList hardRoomListSo;
    [SerializeField] DifficultyDependantRoomList nightmareRoomListSo;
    [Header("")] [SerializeField] ConnectionType connectionType;
    [SerializeField] GameDifficultySO gameDifficultySo;
    [SerializeField] List<GameObject> environmentToggleList;
    public ConnectionDirection connectionDirection; //TODO: remove public

    [System.NonSerialized]public GameObject spawnedRoom;
    List<int> exhaustedNumbers = new List<int>();

    int roomNumber;

    public ConnectionType ConnectionType{
        get => connectionType;
        set{
            connectionType = value;
            if (value is ConnectionType.OpenConnection){
                DeactivateEnvironmentBlockers();
            }
            else if (value is ConnectionType.ClosedConnection || value is ConnectionType.SuspendedConnection){
                ActivateEnvironmentBlockers();
            }
        }
    }

    int attempt;

    void Awake(){
        AssignConnectionDirection();
        if (ConnectionType is ConnectionType.OpenConnection){
            DeactivateEnvironmentBlockers();
        }
        else if (ConnectionType is ConnectionType.ClosedConnection|| ConnectionType is ConnectionType.SuspendedConnection){
            ActivateEnvironmentBlockers();
        }
    }

    void Start(){
        gameDifficultySo.difficultyChangeEvent.AddListener(SetActiveRoomList);
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


    // public Vector3 GetSpawnPosition(){
    //     // var randomRoom = PickRoomToSpawn();
    //     // var randomRoomRoom = randomRoom.GetComponent<Room>();
    //     //
    //     // Vector3 offset = Vector3.zero;
    //     //
    //     // foreach (var connection in randomRoomRoom.connections){
    //     //     if (CheckOppositeDirection(connectionDirection, connection.connectionDirection)){
    //     //         offset = connection.transform.position;
    //     //     }
    //     // }
    //     //
    //     // return transform.position - offset;
    //     return default;
    // }

    int GetRandomNumber(int maxInt){
        return Random.Range(0, maxInt);
    }

    GameObject PickRoomToSpawn(){
        roomNumber = GetRandomNumber(activeRoomListSo.combinedPrefabList.Count);
        while (exhaustedNumbers.Contains(roomNumber) && attempt < activeRoomListSo.combinedPrefabList.Count){
            attempt++;
            roomNumber = GetRandomNumber(activeRoomListSo.combinedPrefabList.Count);
        }
        print(roomNumber);
        print("Count:" + activeRoomListSo.combinedPrefabList.Count);
        var room = activeRoomListSo.combinedPrefabList[roomNumber];
        exhaustedNumbers.Add(roomNumber); //TODO: Not used, but should be!
        return room;
    }

    [ContextMenu("Spawn Room")]
    public void SpawnRoom(){
        attempt = 0;
        while (ConnectionType is ConnectionType.OpenConnection && attempt < activeRoomListSo.combinedPrefabList.Count){
            var randomPrefabRoom = PickRoomToSpawn();
            var randomRoom = randomPrefabRoom.GetComponent<Room>();

            bool validRoom = IsValidRoom(randomRoom, out Vector3 offset);

            if (!validRoom){
                continue;
            }

            Collider[] intersecting = Physics.OverlapBox(transform.position - offset,
                randomPrefabRoom.transform.localScale / 2,Quaternion.identity, LayerMask.GetMask("Validators"));

            if (intersecting.Length > 0){
                ConnectionType = ConnectionType.ClosedConnection;
                break;
            }

            spawnedRoom = Instantiate(randomPrefabRoom, transform.position - offset, quaternion.identity);

            ConnectionType = ConnectionType.UsedConnection;
            //attempt++;

            var spawnedRoomRoom = spawnedRoom.GetComponent<Room>();

            spawnedRoomRoom.spawnedConnection = this;

            foreach (var connection in spawnedRoomRoom.connections){
                if (CheckOppositeDirection(connectionDirection, connection.connectionDirection)){
                    connection.connectionType = ConnectionType.UsedConnection; //TODO: This only affects the prefab, which is really bad. But Needed until we fix Validation.
                }
            }
        }
    }

    bool IsValidRoom(Room room, out Vector3 offset){
        foreach (var connection in room.connections){
            if (CheckOppositeDirection(connectionDirection, connection.connectionDirection)){
                offset = connection.transform.position;
                return true;
            }
        }

        offset = default;
        return false;
    }

    // Vector3 GetOffset(Room room){
    //     foreach (var connection in room.connections){
    //         if (CheckOppositeDirection(connectionDirection, connection.connectionDirection)){
    //            return connection.transform.position;
    //         }
    //     }
    //
    //     return default;
    // }

    bool CheckOppositeDirection(ConnectionDirection direction1, ConnectionDirection direction2){
        if (direction1 is ConnectionDirection.Up && direction2 is ConnectionDirection.Down) return true;
        else if (direction1 is ConnectionDirection.Down && direction2 is ConnectionDirection.Up) return true;
        else if (direction1 is ConnectionDirection.Left && direction2 is ConnectionDirection.Right) return true;
        else if (direction1 is ConnectionDirection.Right && direction2 is ConnectionDirection.Left) return true;
        else return false;
    }

    void DeactivateEnvironmentBlockers(){
        foreach (var _gameObject in environmentToggleList){
            if (_gameObject != null){
                _gameObject.SetActive(false);
            }
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
}
