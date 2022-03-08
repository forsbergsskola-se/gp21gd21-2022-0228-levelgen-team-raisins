using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public enum CounterType
{
    Up,Down
}
public class DisplayTimer : MonoBehaviour
{
    private TextMeshProUGUI text;
    private float counter = 0;
    public CounterType CounterType = CounterType.Up;


    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        text.text = $"Time spent: {minutes} : {seconds}";
    }


    private void Update()
    {
        if (CounterType == CounterType.Up)
        {
            counter += Time.deltaTime;
            DisplayTime(counter);
        }
    }

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        if (CounterType == CounterType.Down)
        {
            DifficultyManager.OnTimeCountDown += DisplayTime;
        }
    }

    private void OnDisable()
    {
        if (CounterType == CounterType.Down)
        {
            DifficultyManager.OnTimeCountDown -= DisplayTime;
        }
    }

}
