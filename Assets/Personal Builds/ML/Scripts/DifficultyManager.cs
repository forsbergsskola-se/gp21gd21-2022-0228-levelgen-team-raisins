using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;



public class DifficultyManager : MonoBehaviour
{

    public static Difficulty difficulty;

    public static int numberDifficultyLevels;
    private  static Timer difficultyTimer;
    //one second = 1000
    public static float timerInterval = 6000;
    public static int currentDifficulty = 0;

    public  delegate void DifficultyChangedDelegate(Difficulty difficulty);

    public static event DifficultyChangedDelegate OnDifficultyChanged;


    private void Start()
    {
        numberDifficultyLevels = Enum.GetValues(typeof(Difficulty)).Length;
        SetTimer();
    }


    private static void SetTimer()
    {
        difficultyTimer = new Timer(timerInterval);

        difficultyTimer.Elapsed += DifficultyUpEvent;
        difficultyTimer.AutoReset = true;
        difficultyTimer.Enabled = true;
    }

    private static void DifficultyChanged()
    {
        if (OnDifficultyChanged != null)
        {
            OnDifficultyChanged(difficulty);
        }
    }
    private static void DifficultyUpEvent(object sender, ElapsedEventArgs e)
    {
        currentDifficulty++;
        if (currentDifficulty < numberDifficultyLevels)
        {
            difficulty = (Difficulty) currentDifficulty;
            DifficultyChanged();
            Debug.Log(difficulty);
        }
        else
        {
            difficultyTimer.Elapsed -= DifficultyUpEvent;
            difficultyTimer.AutoReset = false;
            difficultyTimer.Enabled = false;
        }
    }
}
