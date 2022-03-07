using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.Events;

public enum Difficulty{
    Easy,
    Medium,
    Hard,
    Nightmare
}

[CreateAssetMenu(fileName = "New Game Difficulty", menuName = "Essentials/States/Game Difficulty")]
public class GameDifficultySO : ScriptableObject{

    [System.NonSerialized] public UnityEvent<Difficulty> difficultyChangeEvent;
     public static Difficulty difficulty;


     private static System.Timers.Timer difficultyTimer;
     //one second = 1000
     public static float timerInterval = 4000;
     public static int currentDifficulty = 0;

     public Difficulty Difficulty
    {
        get => difficulty;
        set{
            difficulty = value;
            //Invokes Event when Difficulty is changed
            difficultyChangeEvent.Invoke(value);
        }
    }

     private static void SetTimer()
     {
         difficultyTimer = new System.Timers.Timer(timerInterval);

         difficultyTimer.Elapsed += DifficultyUpEvent;
         difficultyTimer.AutoReset = true;
         difficultyTimer.Enabled = true;
     }

     private static void DifficultyUpEvent(object sender, ElapsedEventArgs e)
     {


         currentDifficulty++;
         difficulty = (Difficulty) currentDifficulty;
     }




    void OnEnable(){
        //Creates event if none exists.
        if (difficultyChangeEvent == null){
            difficultyChangeEvent = new UnityEvent<Difficulty>();
        }

        Difficulty = Difficulty.Easy;
    }

    [ContextMenu("Change Difficulty to Hard")]
    void Test1(){
        Difficulty = Difficulty.Hard;
    }
}
