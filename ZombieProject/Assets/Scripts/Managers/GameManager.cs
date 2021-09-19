using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text roundText;
    public int currentRound;
    private SpawnManager _spawnManager;
    private bool _roundTransition;

    private void Awake()
    {
        _spawnManager = FindObjectOfType<SpawnManager>();
    }

    private void Start()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        currentRound = 1;
        _roundTransition = true;
        UpdateRoundText();
    }
    private void Update()
    {
        if (_spawnManager._endRound && _roundTransition)
        {
            StartCoroutine(RoundCompleted());
        }
    }

    public IEnumerator RoundCompleted()
    {
        _roundTransition = false;
        yield return new WaitForSeconds(5f);
        currentRound++;
        UpdateRoundText();
        yield return new WaitForSeconds(2f);
        _spawnManager.ResetAllCurrentZombies();
        _spawnManager._endRound = false;
        _roundTransition = true;
    }

    public void PlayerDeath()
    {

    }

    private void UpdateRoundText()
    {
        roundText.text = currentRound.ToString();
    }

}
