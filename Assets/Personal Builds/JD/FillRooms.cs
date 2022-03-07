using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FillRooms : MonoBehaviour{
    PrefabListSO spawnedObjects;
    List<Transform> spawnPoints;

    void OnEnable(){
        foreach (var point in GetComponentsInChildren<Transform>()){
            spawnPoints.Add(point);
            var newobject = RandomizeSpawnedObject(point);
            Instantiate(newobject, point.position, point.rotation, transform);
        }
    }

    GameObject RandomizeSpawnedObject(Transform point){
        var spawnedObject = Random.Range(0, spawnedObjects.prefabs.Count);
        var objectToSpawn = spawnedObjects.prefabs[spawnedObject];
        return objectToSpawn;
    }
}
