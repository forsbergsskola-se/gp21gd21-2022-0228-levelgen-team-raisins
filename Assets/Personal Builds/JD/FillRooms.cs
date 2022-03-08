using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FillRooms : MonoBehaviour{
    [SerializeField] DifficultyDependantPrefabList spawnedObjects;
    [SerializeField][Range(0,100)] int chanceToSpawn;

    List<Transform> spawnPoints = new List<Transform>();

    float SpawnProcentage{
        get => chanceToSpawn;
        set => SpawnProcentage = chanceToSpawn / 100;
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.A)){
            OnButtonpressForDebug();
        }
    }

    void OnButtonpressForDebug(){
        var children = GetComponentsInChildren<Transform>();
        var willItSpawnComparator = Random.Range(0f, 1f);
        foreach (var point in children){
            if (point == transform){
                continue;
            }
            if (willItSpawnComparator <= SpawnProcentage){
                spawnPoints.Add(point);
                var newObject = RandomizeSpawnedObject();
                Instantiate(newObject, point.position, point.rotation, point);
            }
        }
    }

    GameObject RandomizeSpawnedObject(){
        var spawnedObject = Random.Range(0, spawnedObjects.combinedPrefabList.Count);
        var objectToSpawn = spawnedObjects.combinedPrefabList[spawnedObject];
        return objectToSpawn;
    }
}
