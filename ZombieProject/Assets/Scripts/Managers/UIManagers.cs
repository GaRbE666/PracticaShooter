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
    [SerializeField] private float timeToShowPlayerStats;
    [SerializeField] private AnimationClip fadeIn;
    [SerializeField] private AnimationClip fadeOut;
    [SerializeField] private Animation fadeAnimation;

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
        FadeOutAnim();
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

    public void FadeInAnim()
    {
        fadeAnimation.clip = fadeIn;
        fadeAnimation.Play();
    }

    public void FadeOutAnim()
    {
        fadeAnimation.clip = fadeOut;
        fadeAnimation.Play();
    }

    public void DisableInGameCanvas()
    {
        inGameCanvas.gameObject.SetActive(false);
    }

    public void ShowPlayersStats()
    {
        StartCoroutine(ShowPlayerStatsCoroutine());
    }

    private IEnumerator ShowPlayerStatsCoroutine()
    {
        yield return new WaitForSeconds(timeToShowPlayerStats);
        scoreCanvas.gameObject.SetActive(true);
    }

    private void UpdatePlayerStats()
    {
        playerScoreText.text = _gameManager.maxScore.ToString();
        playerKillsText.text = _gameManager.totalZombiesKilled.ToString();
        playerHeadShootText.text = _gameManager.headShootCount.ToString();
    }
}
