using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public static float Score = 0.0f;
    public Text CurrentScore;

    void Update()
    {
        CurrentScore.text = Score.ToString("0");
    }

    public void incrementScore()
    {
        Score += 1.0f;
    }

    public void ResetScore()
    {
        Score = 0.0f;
    }
}
