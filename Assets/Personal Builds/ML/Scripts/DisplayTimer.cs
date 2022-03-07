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
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        text.text = $"Time spent: {minutes} : {seconds}";
    }


    private void Update()
    {
        counter += Time.deltaTime;
        DisplayTime(counter);
    }

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    //    DifficultyManager.OnTimeCountDown += DisplayTime;
    }

    private void OnDisable()
    {
    //    DifficultyManager.OnTimeCountDown -= DisplayTime;
    }

}
