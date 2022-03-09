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
    public float increaseEnemyHealthInterval;
    public int enemyHealthIncreaseAmount = 10;

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
        healthTimer.remainingTime = increaseEnemyHealthInterval;
        healthTimer.outOfTime = false;
        OnIncreaseEnemyHealth?.Invoke(enemyHealthIncreaseAmount);
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
        healthTimer = gameObject.AddComponent<MyTimer>();
        healthTimer.remainingTime = increaseEnemyHealthInterval;
        healthTimer.outOfTime = false;
        //    DifficultyManager.OnTimeCountDown += DisplayTime;
    }



    private void OnDisable()
    {
    //    DifficultyManager.OnTimeCountDown -= DisplayTime;
    }

}
