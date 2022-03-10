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
    public int additionalDamageAmount = 0;
    [Range(2,6)]public int MaxObjectsToSpawn = 4;


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
        var objectCount = 0;

        foreach (var p in points)
        {
            if (objectCount == MaxObjectsToSpawn) return;
            if (Random.Range(0, 1f) ! >= SpawnProcentage) continue;
            var randList = Random.Range(0, spawnedObjects.combinedPrefabList.Count);
            var objectToSpawn = spawnedObjects.combinedPrefabList[randList];

            var newObject = Instantiate(objectToSpawn, p.position, Quaternion.identity, p);
            SetupEnemy(newObject);
            objectCount++;
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
