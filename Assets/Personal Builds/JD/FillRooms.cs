using System;
using System.Collections;
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

    public MyTimer spawnTimer;
    public float spawnInterval = 60;
    public int healthAmount = 10;

    private bool healthSet = false;
    public delegate void GetEnemyHealthDelegate();
    public static event GetEnemyHealthDelegate OnGetEnemyHealth;


    public SpawnType SpawnType;

    float SpawnProcentage => (float) chanceToSpawn/100f;

    private void Start()
    {
        DisplayTimer.OnIncreaseEnemyHealth += SetEnemyHealth;

        OnGetEnemyHealth?.Invoke();

        if (SpawnType == SpawnType.OnTimer)
        {
            spawnTimer = gameObject.AddComponent<MyTimer>();
            spawnTimer.remainingTime = spawnInterval;
            spawnTimer.outOfTime = false;
        }

        StartCoroutine(LateSpawn());
    }

    IEnumerator LateSpawn()
    {
        yield return new WaitForSeconds(0.3f);
        if(SpawnType == SpawnType.OnStart)
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

        foreach (var p in points)
        {
            if (Random.Range(0, 1f) ! >= SpawnProcentage) continue;
            var randList = Random.Range(0, spawnedObjects.combinedPrefabList.Count);
            var objectToSpawn = spawnedObjects.combinedPrefabList[randList];

            var newObject = Instantiate(objectToSpawn, p.position, Quaternion.identity, p);
            SetupEnemy(newObject);
        }
    }

    private void SetEnemyHealth(int setHealth)
    {
        healthAmount = setHealth;
    }

    private void SetupEnemy(GameObject enemy)
    {
        if (enemy.GetComponent<NetworkObject>() == null) return;
        enemy.GetComponent<NetworkObject>().Spawn();

        if (enemy.GetComponent<NetworkCharacterState>() == null) return;
        enemy.GetComponent<NetworkCharacterState>().HitPoints += healthAmount;
    }

    GameObject RandomizeSpawnedObject(){
        var spawnedObject = Random.Range(0, spawnedObjects.combinedPrefabList.Count);
        var objectToSpawn = spawnedObjects.combinedPrefabList[spawnedObject];
        SetupEnemy(objectToSpawn);
        return objectToSpawn;
    }
}
