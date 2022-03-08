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

    private List<Transform> spawnPoints = new List<Transform>();

    public void SpawnEnemy()
    {
      var enemyNum =  Random.Range(0, currentEnemies.Count);
      var spawnNum = Random.Range(0, spawnPoints.Count);
      Instantiate(currentEnemies[enemyNum], spawnPoints[spawnNum].position, Quaternion.identity);
    }


    public void SpawnRandomNumberEnemies(int maxNumber)
    {
        var pointsToSpawn = GetComponentsInChildren<Transform>()
            .Where(x => x.CompareTag("EnemySpawnPoints")).ToList();

        for (int i = 0; i < maxNumber; i++)
        {
            var spawnOrNot = Random.Range(0, 2);

            if (spawnOrNot == 1)
            {
                var enemyNum =  Random.Range(0, currentEnemies.Count);
                var spawnNum = Random.Range(0, pointsToSpawn.Count);
                Instantiate(currentEnemies[enemyNum], pointsToSpawn[spawnNum].position, Quaternion.identity);
            }
        }
    }

    void Start()
    {
        DifficultyManager.OnDifficultyChanged += ChangeEnemyTypes;
        ChangeEnemyTypes(Difficulty.Easy);
    //    SetSpawnPoints();
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

}
