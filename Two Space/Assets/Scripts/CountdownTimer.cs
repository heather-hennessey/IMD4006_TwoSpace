﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public float StartTime = 120f;
    float CurrentTime = 0f;

    public Text CountdownTimerText;
    public Text CurrentScore;

    void Start()
    {
        CurrentTime = StartTime;
    }

    void Update()
    {
        CurrentTime -= 1 * Time.deltaTime;
        CountdownTimerText.text = CurrentTime.ToString("0");

        if(CurrentTime <= 0)
        {
            CurrentTime = 0;
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}
