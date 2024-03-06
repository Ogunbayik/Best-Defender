using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] private TextMeshProUGUI scoreText;

    [HideInInspector]
    public int currentScore;
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);

        instance = this;
    }
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
