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
    public string name;


    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        text.text = $"{name}: {minutes} : {seconds}";
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
        SetName();
        text = GetComponent<TextMeshProUGUI>();
        if (CounterType == CounterType.Down)
        {
            DifficultyManager.OnTimeCountDown += DisplayTime;
        }
    }

    private void SetName()
    {
        if (CounterType == CounterType.Up)
        {
            name = "Time spent";
        }
        else if (CounterType == CounterType.Down)
        {
            name = "Time Left";
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
