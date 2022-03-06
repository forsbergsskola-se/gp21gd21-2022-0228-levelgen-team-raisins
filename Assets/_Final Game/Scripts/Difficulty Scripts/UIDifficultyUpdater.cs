using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIDifficultyUpdater : MonoBehaviour{
    [SerializeField] GameDifficultySO gameDifficultySo;

    TextMeshProUGUI textMeshProUGUI;
    void Awake(){
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable(){
        gameDifficultySo.difficultyChangeEvent.AddListener(SetDifficultyText);
        SetDifficultyText(gameDifficultySo.Difficulty);
    }
    void OnDisable(){
        gameDifficultySo.difficultyChangeEvent.RemoveListener(SetDifficultyText);
    }

    void SetDifficultyText(Difficulty difficulty){
        textMeshProUGUI.text = "Difficulty: " + difficulty;
    }


}
