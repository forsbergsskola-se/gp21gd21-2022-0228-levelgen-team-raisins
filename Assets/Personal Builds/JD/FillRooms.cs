using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Multiplayer.Samples.BossRoom;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

public class FillRooms : MonoBehaviour{
    [SerializeField] DifficultyDependantPrefabList spawnedObjects;
    [SerializeField][Range(0,100)] int chanceToSpawn;

    List<Transform> spawnPoints = new List<Transform>();

    public int healthAmount = 10;

    float SpawnProcentage{
        get => chanceToSpawn;
        set => SpawnProcentage = chanceToSpawn / 100;
    }

    private void Start()
    {
        DisplayTimer.OnIncreaseEnemyHealth += SetEnemyHealth;
        SpawnOnStart(1);
    }

    private void OnDisable()
    {
        DisplayTimer.OnIncreaseEnemyHealth -= SetEnemyHealth;
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


    private void SpawnOnStart(int objectsToSpawn)
    {
        var points = GetComponentsInChildren<Transform>()
            .Where(x => x.CompareTag("EnemySpawnPoints")).ToList();

        for (int i = 0; i < objectsToSpawn; i++)
        {
            var newObject = Instantiate(spawnedObjects.prefabLists[0].prefabs[0], points[0].position, Quaternion.identity);
            SetEnemyHealth(newObject);
        }
    }

    public void SetEnemyHealth(int increaseAmount)
    {
        healthAmount = increaseAmount;
    }

    private void SetEnemyHealth(GameObject enemy)
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
        SetEnemyHealth(objectToSpawn);
        return objectToSpawn;
    }
}
