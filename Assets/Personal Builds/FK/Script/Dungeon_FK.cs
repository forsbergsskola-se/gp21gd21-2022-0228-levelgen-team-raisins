using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Dungeon_FK : MonoBehaviour{
    [SerializeField] List<GameObject> rooms;

    void Update(){

        if (runInEditMode && Input.GetKeyDown(KeyCode.A)){
            SpawnRoom();
        }
    }

    [ContextMenu("Spawn Room")]
    void SpawnRoom(){
        Instantiate(rooms[0], transform.position, quaternion.identity);
    }
}
