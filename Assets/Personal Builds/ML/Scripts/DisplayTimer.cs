using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class DisplayTimer : MonoBehaviour
{
    private TextMeshProUGUI text;
    private float counter = 0;
    public float increaseEnemyHealthInterval = 4;
    private int currentEnemyHealth = 10;
    public int enemyHealthIncreaseAmount = 5;

    private MyTimer healthTimer;
    public delegate void IncreaseEnemyHealthDelegate(int increaseAmount);
    public static event IncreaseEnemyHealthDelegate OnIncreaseEnemyHealth;

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        text.text = $"Time spent: {minutes} : {seconds}";
    }

    private void IncreaseEnemyHealth()
    {
        currentEnemyHealth += enemyHealthIncreaseAmount;
        healthTimer.remainingTime = increaseEnemyHealthInterval;
        healthTimer.outOfTime = false;
        OnIncreaseEnemyHealth?.Invoke(currentEnemyHealth);
    }

    private void Update()
    {
        counter += Time.deltaTime;
        DisplayTime(counter);

        if (healthTimer.outOfTime)
        {
           IncreaseEnemyHealth();
        }
    }

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        FillRooms.OnGetEnemyHealth += SendEnemyHealth;
        healthTimer = gameObject.AddComponent<MyTimer>();
        healthTimer.remainingTime = increaseEnemyHealthInterval;
        healthTimer.outOfTime = false;
        //    DifficultyManager.OnTimeCountDown += DisplayTime;
    }


    private void SendEnemyHealth()
    {
        OnIncreaseEnemyHealth?.Invoke(currentEnemyHealth);
    }

    private void OnDisable()
    {
        FillRooms.OnGetEnemyHealth -= SendEnemyHealth;
    //    DifficultyManager.OnTimeCountDown -= DisplayTime;
    }

}
