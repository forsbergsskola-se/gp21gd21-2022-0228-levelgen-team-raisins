using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    private static Difficulty difficulty;

    private  int numberDifficultyLevels;
    public float timerInterval = 6;
    private  int currentDifficulty = 0;

    private MyTimer timer;

    public  delegate void DifficultyChangedDelegate(Difficulty difficulty);
    public static event DifficultyChangedDelegate OnDifficultyChanged;


    private void OnEnable()
    {
        timer = GetComponent<MyTimer>();
        timer.remainingTime = timerInterval;
        numberDifficultyLevels = Enum.GetValues(typeof(Difficulty)).Length;
    }

    private void Update()
    {
        if (timer.outOfTime)
        {
            DifficultyUpEvent();
            timer.remainingTime = timerInterval;
            timer.outOfTime = false;
        }
    }

    private void DifficultyUpEvent()
    {
        currentDifficulty++;

        if (currentDifficulty < numberDifficultyLevels)
        {
            difficulty = (Difficulty) currentDifficulty;
            DifficultyChanged();
        }
    }

    private static void DifficultyChanged()
    {
        if (OnDifficultyChanged != null)
        {
            OnDifficultyChanged(difficulty);
        }
    }
}
