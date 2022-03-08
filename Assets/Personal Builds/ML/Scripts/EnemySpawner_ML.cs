using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemySpawner_ML : MonoBehaviour
{
    [SerializeField] private PrefabListSO EasyEnemies;
    [SerializeField] private PrefabListSO MediumEnemies;
    [SerializeField] private PrefabListSO HardEnemies;
    [SerializeField] private PrefabListSO NightmareEnemies;
    private List<GameObject> currentEnemies;

    public float spawnInterval = 5;
    private MyTimer spawnTimer;

    private List<Transform> spawnPoints = new List<Transform>();

    public void SpawnEnemy()
    {
      var enemyNum =  Random.Range(0, currentEnemies.Count);
      var spawnNum = Random.Range(0, spawnPoints.Count);
      Instantiate(currentEnemies[enemyNum], spawnPoints[spawnNum].position, Quaternion.identity);
    }

    void Start()
    {
        spawnTimer = GetComponent<MyTimer>();
        spawnTimer.remainingTime = spawnInterval;
        spawnTimer.outOfTime = false;
        DifficultyManager.OnDifficultyChanged += ChangeEnemyTypes;
        ChangeEnemyTypes(Difficulty.Easy);
        SetSpawnPoints();
    }


    private void SetSpawnPoints()
    {
      spawnPoints =  GameObject.FindGameObjectsWithTag("Room")[0]
            .GetComponentsInChildren<Transform>()
            .Where(x => x.CompareTag("EnemySpawnPoints")).ToList();
    }

    private void OnDisable()
    {
        DifficultyManager.OnDifficultyChanged -= ChangeEnemyTypes;
    }

    private void ChangeEnemyTypes(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                currentEnemies = EasyEnemies.prefabs;
                break;
            case Difficulty.Medium:
                currentEnemies = MediumEnemies.prefabs;
                break;
            case Difficulty.Hard:
                currentEnemies = HardEnemies.prefabs;
                break;
            case Difficulty.Nightmare:
                currentEnemies = NightmareEnemies.prefabs;
                break;
        }
    }

    void Update()
    {
        if (spawnTimer.outOfTime)
        {
            SpawnEnemy();
            spawnTimer.remainingTime = spawnInterval;
            spawnTimer.outOfTime = false;
        }
    }
}
