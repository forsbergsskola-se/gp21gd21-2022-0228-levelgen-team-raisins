using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemySpawner_ML : MonoBehaviour
{
    [SerializeField] private ScriptableObject EasyEnemies;
    [SerializeField] private ScriptableObject MediumEnemies;
    [SerializeField] private ScriptableObject HardEnemies;
    [SerializeField] private ScriptableObject NightmareEnemies;

    void Start()
    {
        DifficultyManager.OnDifficultyChanged += ChangeEnemyTypes;
        ChangeEnemyTypes(Difficulty.Easy);
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
                break;
            case Difficulty.Medium:
                break;
            case Difficulty.Hard:
                break;
            case Difficulty.Nightmare:
                break;
        }
    }

    void Update()
    {

    }
}
