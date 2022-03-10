using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public enum DifficultyIncreaseType
{
    Health, Damage, HealthAndDamage, None
}


public class DisplayTimer : MonoBehaviour
{
    private TextMeshProUGUI text;
    private float counter = 0;
    public float increaseEnemyDifficultyInterval = 500;

    public int enemyStartHealth = 10;
    public int enemyHealthIncreaseAmount = 5;
    public int enemyDamageIncreaseAmount = 5;
  //  public int enemyStartDamage = 5;

    public DifficultyIncreaseType DifficultyIncreaseType;
    private MyTimer healthTimer;
    public delegate void IncreaseEnemyDifficltyDelegate(int increaseAmount);
    public static event IncreaseEnemyDifficltyDelegate OnIncreaseEnemyHealth;



    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        text.text = $"Time spent: {minutes} : {seconds}";
    }

    private void IncreaseEnemyDifficulty()
    {
        enemyStartHealth += enemyHealthIncreaseAmount;
        healthTimer.remainingTime = increaseEnemyDifficultyInterval;
        healthTimer.outOfTime = false;
        OnIncreaseEnemyHealth?.Invoke(enemyStartHealth);
    }

    private void Update()
    {
        counter += Time.deltaTime;
        DisplayTime(counter);

        if (healthTimer.outOfTime)
        {
           IncreaseEnemyDifficulty();
        }
    }

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        healthTimer = gameObject.AddComponent<MyTimer>();
        healthTimer.remainingTime = increaseEnemyDifficultyInterval;
        healthTimer.outOfTime = false;
        //    DifficultyManager.OnTimeCountDown += DisplayTime;
    }




    private void OnDisable()
    {
    //    DifficultyManager.OnTimeCountDown -= DisplayTime;
    }

}
