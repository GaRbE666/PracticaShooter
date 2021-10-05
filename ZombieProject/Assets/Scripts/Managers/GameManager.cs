using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text roundText;
    [SerializeField] private bool noZombies;
    [SerializeField] private Transform startSpawn;
    [SerializeField] private bool spawnInStart;
    [SerializeField] private EnemyScriptable enemyScriptable;
    [SerializeField] private float travellingTime;
    [SerializeField] private float timeToResetGame;
    [SerializeField] private float timePlayerDeathAnim;
    public ZombieSpawnTable[] zombieSpawnTable;
    public int totalZombiesKilled;
    public int maxScore;
    public int headShootCount;
    public int currentRound;
    public bool powerOn;
    public bool playerTeleported;

    private CameraTravelling _cameraTravelling;
    private PowerOn _powerOnMethod;
    private SpawnManager _spawnManager;
    private bool _roundTransition;
    private PlayerMovement _playerMovement;
    private PlayerHealth _playerHealth;
    private AudioManager _audioManager;
    private UIManagers _uiManager;

    private void Awake()
    {
        _spawnManager = FindObjectOfType<SpawnManager>();
        _powerOnMethod = FindObjectOfType<PowerOn>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _playerHealth = FindObjectOfType<PlayerHealth>();
        _audioManager = FindObjectOfType<AudioManager>();
        _cameraTravelling = FindObjectOfType<CameraTravelling>();
        _uiManager = FindObjectOfType<UIManagers>();
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
        enemyScriptable.maxHealth = enemyScriptable.startedHealth;
        _powerOnMethod.PowerOnReleased += TurnOnThePower;
        _playerHealth.PlayerDieRelease += ResetGame;
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

    private void ResetGame()
    {
        StartCoroutine(ResetGameCoroutine());
    }

    private IEnumerator ResetGameCoroutine()
    {
        yield return new WaitForSeconds(timePlayerDeathAnim);
        SelectRandomTravelling();
        yield return new WaitForSeconds(travellingTime);
        _uiManager.FadeInAnim();
        yield return new WaitForSeconds(timeToResetGame);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void SelectRandomTravelling()
    {
        int numRand = Random.Range(0, 2);
        if (numRand == 0)
        {
            _cameraTravelling.PlayTravEsce();
        }
        else
        {
            _cameraTravelling.PlayTravBar();
        }
    }

    public IEnumerator RoundCompleted()
    {
        _roundTransition = false;
        _audioManager.PlayEndRoundSound();
        yield return new WaitForSeconds(9f);
        _audioManager.PlayStartRound();
        currentRound++;
        UpdateRoundText();
        yield return new WaitForSeconds(12f);
        RoundUp();
        _spawnManager.ResetAllCurrentZombies();
        _spawnManager._endRound = false;
        _roundTransition = true;
    }

    private void RoundUp()
    {
        if (currentRound <= 50)
        {
            enemyScriptable.maxHealth += 25;
        }
        UpdateMaxZombiesPerRound();
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
