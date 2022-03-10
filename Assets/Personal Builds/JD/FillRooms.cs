using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Multiplayer.Samples.BossRoom;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

public enum SpawnType
{
    OnStart, OnTimer
}

public class FillRooms : MonoBehaviour{
    [SerializeField] DifficultyDependantPrefabList spawnedObjects;
    [SerializeField][Range(0,100)] int chanceToSpawn;

    List<Transform> spawnPoints = new List<Transform>();

    public MyTimer spawnTimer;
    public float spawnInterval = 60;
    public int healthAmount = 10;
    [Range(2, 6)]public int MaxObjectsToSpawn = 4;
    public SpawnType SpawnType;

    float SpawnProcentage => (float) chanceToSpawn/100f;

    private void Start()
    {
        DisplayTimer.OnIncreaseEnemyHealth += SetEnemyHealth;

        if (SpawnType == SpawnType.OnTimer)
        {
            spawnTimer = gameObject.AddComponent<MyTimer>();
            spawnTimer.remainingTime = spawnInterval;
            spawnTimer.outOfTime = false;
        }

        else if(SpawnType == SpawnType.OnStart)
        {
            SpawnRandNumberObjects();
        }
    }

    private void OnDisable()
    {
        DisplayTimer.OnIncreaseEnemyHealth -= SetEnemyHealth;
    }


    void Update()
    {
        if (SpawnType == SpawnType.OnTimer)
            if (spawnTimer.outOfTime)
                SpawnOnTimer();
    }

    private void SpawnOnTimer()
    {
        spawnTimer.outOfTime = false;
        spawnTimer.remainingTime = spawnInterval;
        SpawnRandNumberObjects();
    }

    private void SpawnRandNumberObjects()
    {
        var points = GetComponentsInChildren<Transform>().ToList();

        for (var i = 0; i < MaxObjectsToSpawn; i++)
        {
            if (Random.Range(0, 1f) ! >= SpawnProcentage) continue;

            var randPoint= Random.Range(0, points.Count);
            var randList= Random.Range(0, spawnedObjects.prefabLists.Count);
            var randPrefab= Random.Range(0, spawnedObjects.prefabLists[randList].prefabs.Count);
            var objectToSpawn = spawnedObjects.prefabLists[randList].prefabs[randPrefab];

            var newObject = Instantiate(objectToSpawn, points[randPoint].position, Quaternion.identity, points[i]);
            SetupEnemy(newObject);
        }
    }

    private void SetEnemyHealth(int setHealth)
    {
        healthAmount = setHealth;
    }

    private void SetupEnemy(GameObject enemy)
    {
        if (enemy.GetComponent<CharacterClassContainer>() == null) return;

        enemy.GetComponent<NetworkObject>().Spawn();
        var theHealth = ScriptableObject.CreateInstance<IntVariable>();
        theHealth.Value = healthAmount;
        enemy.GetComponent<CharacterClassContainer>().CharacterClass.BaseHP = theHealth;
    }

    GameObject RandomizeSpawnedObject(){
        var spawnedObject = Random.Range(0, spawnedObjects.combinedPrefabList.Count);
        var objectToSpawn = spawnedObjects.combinedPrefabList[spawnedObject];
        SetupEnemy(objectToSpawn);
        return objectToSpawn;
    }
}
