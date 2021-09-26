using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text roundText;
    [SerializeField] private bool noZombies;
    [SerializeField] private Transform startSpawn;
    [SerializeField] private bool spawnInStart;
    [SerializeField] private EnemyScriptable enemyScriptable;
    public ZombieSpawnTable[] zombieSpawnTable;
    public int currentRound;
    public bool powerOn;
    public bool playerTeleported;
    private PowerOn _powerOnMethod;
    private SpawnManager _spawnManager;
    private bool _roundTransition;
    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _spawnManager = FindObjectOfType<SpawnManager>();
        _powerOnMethod = FindObjectOfType<PowerOn>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Start()
    {
        InitializeVariables();
        if (spawnInStart)
        {
            _playerMovement.GetComponent<CharacterController>().enabled = false;
            _playerMovement.transform.position = startSpawn.position;
            _playerMovement.GetComponent<CharacterController>().enabled = true;
        }
    }

    private void InitializeVariables()
    {
        currentRound = 1;
        _roundTransition = true;
        _powerOnMethod.PowerOnReleased += TurnOnThePower;
        UpdateRoundText();
        UpdateMaxZombiesPerRound();
    }
    private void Update()
    {
        if (!noZombies)
        {
            if (_spawnManager._endRound && _roundTransition)
            {
                StartCoroutine(RoundCompleted());
            }
        }
    }

    private void UpdateMaxZombiesPerRound()
    {
        for (int i = 0; i < zombieSpawnTable.Length; i++)
        {
            if ((currentRound - 1) == i)
            {
                _spawnManager.maxZombiesPerRound = zombieSpawnTable[i].maxZombiesPerRoundTab;
                _spawnManager.maxZombiesInScene = zombieSpawnTable[i].maxZombiesInSceneTab;
            }
        }
    }

    public IEnumerator RoundCompleted()
    {
        _roundTransition = false;
        yield return new WaitForSeconds(5f);
        RoundUp();
        yield return new WaitForSeconds(2f);
        _spawnManager.ResetAllCurrentZombies();
        _spawnManager._endRound = false;
        _roundTransition = true;
    }

    private void RoundUp()
    {
        currentRound++;
        if (currentRound <= 50)
        {
            enemyScriptable.maxHealth += 25;
        }
        UpdateMaxZombiesPerRound();
        UpdateRoundText();
    }

    public void PlayerDeath()
    {

    }

    private void UpdateRoundText()
    {
        roundText.text = currentRound.ToString();
    }

    private void TurnOnThePower()
    {
        powerOn = true;
    }

}
