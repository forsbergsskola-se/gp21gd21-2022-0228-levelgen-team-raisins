using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    private TextMeshProUGUI text;
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        text.text = $"Time Left: {minutes} : {seconds}";
    }

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        DifficultyManager.OnTimeCountDown += DisplayTime;
    }

    private void OnDisable()
    {
        DifficultyManager.OnTimeCountDown -= DisplayTime;
    }

}
