using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagers : MonoBehaviour
{
    [SerializeField] private Canvas inGameCanvas;
    [SerializeField] private Canvas scoreCanvas;
    [SerializeField] private Text playerScoreText;
    [SerializeField] private Text playerKillsText;
    [SerializeField] private Text playerHeadShootText;

    private GameManager _gameManager;
    private PlayerHealth _playerhealth;

    private void Awake()
    {
        _playerhealth = FindObjectOfType<PlayerHealth>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        scoreCanvas.gameObject.SetActive(false);
        _playerhealth.PlayerDieRelease += ShowPlayersStats;
    }

    private void Update()
    {
        if (!_playerhealth._isDie)
        {
            if (Input.GetKey(KeyCode.Tab))
            {
                UpdatePlayerStats();
                scoreCanvas.gameObject.SetActive(true);
            }
            else
            {
                scoreCanvas.gameObject.SetActive(false);
            }
        }
    }

    public void DisableInGameCanvas()
    {
        inGameCanvas.gameObject.SetActive(false);
    }

    public void ShowPlayersStats()
    {
        scoreCanvas.gameObject.SetActive(true);
    }

    private void UpdatePlayerStats()
    {
        playerScoreText.text = _gameManager.maxScore.ToString();
        playerKillsText.text = _gameManager.totalZombiesKilled.ToString();
        playerHeadShootText.text = _gameManager.headShootCount.ToString();
    }
}
