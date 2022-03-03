using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Dungeon_FK : MonoBehaviour{
    [SerializeField] List<GameObject> rooms;
    [SerializeField] List<Room_FK> RoomFks;


    void Awake(){
        for (int i = 0; i < rooms.Count; i++){
            RoomFks[i] = rooms[i].GetComponent<Room_FK>();
        }
    }

    [ContextMenu("Spawn Rooms")]
    IEnumerator SpawnRoom(){
        for (int i = 0; i < rooms.Count; i++){
            Instantiate(rooms[i], transform.position, quaternion.identity);
            rooms[i].transform.position = RoomFks[i - 1].connections[i].transform.position + RoomFks[i].connections[1].transform.position;
            yield return new WaitForSeconds(1);
        }
    }
}

