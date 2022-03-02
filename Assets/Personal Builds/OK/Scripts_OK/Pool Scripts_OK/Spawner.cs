using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] float timeToSpawn = 0f;
    float timeSinceSpawn;
    ObjectPool _objectPool;
    [SerializeField] GameObject prefab;
    [SerializeField] int initialSize;
    [SerializeField] float spawnSpreadRange;
    [SerializeField] bool isSpawnRateMultiplierActive;
    [SerializeField] float spawnRate = 1;

    void Start()
    {
        _objectPool = FindObjectOfType<ObjectPool>();
        
        for (int i = 0; i < initialSize; i++)
        {
            var returnedObject =_objectPool.GetObject(prefab,transform);
            returnedObject.transform.position = transform.position + RandomLocation();
            returnedObject.SetActive(false);
        }
    }

    void Update()
    {
        timeSinceSpawn += Time.deltaTime;
        if (timeSinceSpawn >= timeToSpawn)
        {
            if (isSpawnRateMultiplierActive)
            {
                for (var i = 0; i < spawnRate; i++)
                {
                    SpawnFromPool();
                }
            }
            else
            {
                SpawnFromPool();
            }
            
        }
    }
    
    Vector3 RandomLocation(){
        //may change x and z between 1-3?
        var position = new Vector3((Random.insideUnitCircle.x * spawnSpreadRange), 0f,
            (Random.insideUnitSphere.z * spawnSpreadRange));
        return position;
    }

    void SpawnFromPool(){
        GameObject newGameObject = _objectPool.GetObject(prefab,transform);
        newGameObject.SetActive(true);
        newGameObject.transform.position = this.transform.position + RandomLocation();
        
        timeSinceSpawn = 0f;
}
    
#if UNITY_EDITOR
    void OnDrawGizmosSelected(){
        Handles.DrawWireDisc(transform.position, transform.up,spawnSpreadRange);
        
    }
#endif
}
