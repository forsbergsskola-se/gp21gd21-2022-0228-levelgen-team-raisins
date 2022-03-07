using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TMPro;
using UnityEngine;

public class UIDifficultyUpdater : MonoBehaviour{
    private  static Timer difficultyTimer;
    public static int numberDifficultyLevels;
    //one second = 1000
    public static float timerInterval = 6000;
    public static int currentDifficulty = 0;


    [SerializeField]  GameDifficultySO gameDifficultySo;

    TextMeshProUGUI textMeshProUGUI;
    void Awake(){
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        DifficultyManager.OnDifficultyChanged += SetDifficultyText;
    }

    void OnEnable(){
        gameDifficultySo.difficultyChangeEvent.AddListener(SetDifficultyText);
        SetDifficultyText(gameDifficultySo.Difficulty);
    }
    void OnDisable(){
        gameDifficultySo.difficultyChangeEvent.RemoveListener(SetDifficultyText);
        DifficultyManager.OnDifficultyChanged -= SetDifficultyText;
    }

    void SetDifficultyText(Difficulty difficulty)
    {
        textMeshProUGUI.text = $"Difficulty: {difficulty} \n";
    }




}
