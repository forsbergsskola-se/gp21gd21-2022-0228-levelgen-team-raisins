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

     public static int numberDifficultyLevels;
     private  static Timer difficultyTimer;
     //one second = 1000
     public static float timerInterval = 6000;
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
         difficultyTimer = new Timer(timerInterval);

         difficultyTimer.Elapsed += DifficultyUpEvent;
         difficultyTimer.AutoReset = true;
         difficultyTimer.Enabled = true;
     }

     private static void DifficultyUpEvent(object sender, ElapsedEventArgs e)
     {
         currentDifficulty++;
         if (currentDifficulty < numberDifficultyLevels)
         {
             difficulty = (Difficulty) currentDifficulty;
             Debug.Log(difficulty);
         }
     }




    void OnEnable(){
        //Creates event if none exists.
        if (difficultyChangeEvent == null){
            difficultyChangeEvent = new UnityEvent<Difficulty>();
        }

        Difficulty = Difficulty.Easy;
        numberDifficultyLevels = Enum.GetValues(typeof(Difficulty)).Length;

        SetTimer();
    }

    [ContextMenu("Change Difficulty to Hard")]
    void Test1(){
        Difficulty = Difficulty.Hard;
    }
}
