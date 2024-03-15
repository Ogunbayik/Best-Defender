using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gemText;

    [HideInInspector]
    public int currentScore;
    [HideInInspector]
    public int currentGemCount;
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);

        instance = this;
    }
    void Start()
    {
        currentScore = 0;
        currentGemCount = 0;

        scoreText.text = "Score: " + currentScore;
        gemText.text = currentGemCount.ToString();
    }

    public void AddScore(int score)
    {
        currentScore += score;
        scoreText.text = "Score: " + currentScore;
    }

    public void AddGem(int count)
    {
        currentGemCount += count;
    }
}
