using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FillRooms : MonoBehaviour{
    [SerializeField] PrefabListSO spawnedObjects;
    [SerializeField][Range(0,100)] int chanceToSpawn;

    List<Transform> spawnPoints = new List<Transform>();


    void Update(){
        if (Input.GetKeyDown(KeyCode.A)){
            OnButtonpressForDebug();
        }
    }

    void OnButtonpressForDebug(){
        var children = GetComponentsInChildren<Transform>();
        foreach (var point in children){
            spawnPoints.Add(point);
            var newObject = RandomizeSpawnedObject();
            Instantiate(newObject, point.position, point.rotation, point);
        }
    }

    GameObject RandomizeSpawnedObject(){
        var spawnedObject = Random.Range(0, spawnedObjects.prefabs.Count);
        var objectToSpawn = spawnedObjects.prefabs[spawnedObject];
        return objectToSpawn;
    }
}
