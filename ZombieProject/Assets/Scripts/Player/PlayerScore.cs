using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private Text scoreText;

    private void Start()
    {
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    public void QuitScore(int amount)
    {
        score -= amount;
        UpdateScoreText();
    }
}
