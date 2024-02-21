using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    [HideInInspector]
    public int currentScore;
    void Start()
    {
        currentScore = 0;
        scoreText.text = "Score: " + currentScore;
    }

    public void AddScore(int score)
    {
        currentScore += score;
        scoreText.text = "Score: " + currentScore;
    }
}
