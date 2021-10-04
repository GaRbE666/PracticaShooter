using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagers : MonoBehaviour
{
    [SerializeField] private Canvas inGameCanvas;
    [SerializeField] private Canvas scoreCanvas;

    private PlayerHealth _playerhealth;

    private void Awake()
    {
        _playerhealth = FindObjectOfType<PlayerHealth>();
    }

    private void Start()
    {
        _playerhealth.PlayerDieRelease += ShowPlayersStats;
    }

    public void DisableInGameCanvas()
    {
        inGameCanvas.gameObject.SetActive(false);
    }

    public void ShowPlayersStats()
    {
        scoreCanvas.gameObject.SetActive(true);
    }
}
