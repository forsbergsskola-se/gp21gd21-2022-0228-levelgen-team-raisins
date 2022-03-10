using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    private static Difficulty difficulty;

    private  int numberDifficultyLevels;
    [Tooltip("Time in seconds until Difficulty Increase")] public float timerInterval = 6;
    private  int currentDifficulty = 0;

    private MyTimer timer;

    public  delegate void DifficultyChangedDelegate(Difficulty difficulty);
    public static event DifficultyChangedDelegate OnDifficultyChanged;

    public delegate void TimerCountdownDelegate(float remainingTime);

    public static event TimerCountdownDelegate OnTimeCountDown;

    public GameDifficultySO difficultySo;


    private void OnEnable()
    {
        timer = GetComponent<MyTimer>();
        timer.remainingTime = timerInterval;
        numberDifficultyLevels = Enum.GetValues(typeof(Difficulty)).Length;
    }

    private void Update()
    {
        Countdown();
        if (timer.outOfTime)
        {
            DifficultyUpEvent();
            timer.remainingTime = timerInterval;
            timer.outOfTime = false;
        }
    }

    private void Countdown()
    {
        OnTimeCountDown?.Invoke(timer.remainingTime);
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

    private void DifficultyChanged()
    {
        difficultySo.difficultyChangeEvent?.Invoke(difficulty);
        difficultySo.Difficulty = difficulty; //Its very chaotic when there's 2 different stored difficulty values. Adding this to try and solve connection issue
        OnDifficultyChanged?.Invoke(difficulty);
    }
}
