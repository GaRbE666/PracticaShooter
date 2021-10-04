using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    public int score;
    [SerializeField] private Text scoreText;

    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        UpdateScoreText();
        _gameManager.maxScore = score;
    }

    private void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    public void AddScore(int amount)
    {
        score += amount;
        _gameManager.maxScore += amount;
        UpdateScoreText();
    }

    public void QuitScore(int amount)
    {
        score -= amount;
        UpdateScoreText();
    }
}
