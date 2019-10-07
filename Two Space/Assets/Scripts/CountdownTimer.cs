using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    float StartTime = 100f;
    float CurrentTime = 0f;

    public Text CountdownTimerText;

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
