using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("References")]
    public List<GameObject> spawns;
    [SerializeField] private GameObject zombie;
    [SerializeField] private Transform ZombieContainer;

    [Header("Parameters")]
    public int maxZombiesPerRound;
    public int maxZombiesInScene;
    [SerializeField] private float maxTimeToSpawn;
    [SerializeField] private float minTimeToSpawn;
    [SerializeField] private float maxDistanceToPlayerToCanSpawn;
    [SerializeField] private float timeBeforeStartSpawn;

    [HideInInspector] public int visitedRooms;
    [HideInInspector] public int currentZombiesInScene;
    [HideInInspector] public int currentZombiesPerRound;
    private bool _canSpawn;
    private bool _canSpawnMore;
    private float _timeRandomToSpawn;
    private GameManager _gameManager;
    [HideInInspector] public bool _endRound;
    private PlayerMovement _playerMovement;
    [HideInInspector] public List<GameObject> spawnsActived;

    private void Awake()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(timeBeforeStartSpawn);
        _canSpawnMore = true;
        _canSpawn = true;
    }

    private void Update()
    {
        CheckNumberMaxOfZombiesInScene();
        CheckMaxZombiesPerRound();
        if (_canSpawn && _canSpawnMore && !_endRound && !_gameManager.playerTeleported)
        {
            StartCoroutine(SpawnZombies());
        }
    }

    private void CheckNumberMaxOfZombiesInScene()
    {
        if (currentZombiesInScene >= maxZombiesInScene || currentZombiesPerRound == maxZombiesPerRound)
        {
            _canSpawnMore = false;
        }
        else
        {
            _canSpawnMore = true;
        }
    }

    private void CheckMaxZombiesPerRound()
    {
        if (currentZombiesPerRound >= maxZombiesPerRound && currentZombiesInScene == 0)
        {
            _endRound = true;
        }
        else
        {
            _endRound = false;
        }
    }

    private IEnumerator SpawnZombies()
    {
        _canSpawn = false;
        _timeRandomToSpawn = Random.Range(minTimeToSpawn, maxTimeToSpawn + 1);
        SpawnOneZombie();
        yield return new WaitForSeconds(_timeRandomToSpawn);
        _canSpawn = true;
    }

    private void SpawnOneZombie()
    {
        int spawnRandom = SelectOneSpawn();
        GameObject zombieClone = Instantiate(zombie, spawnsActived[spawnRandom].transform.position, spawnsActived[spawnRandom].transform.rotation);
        zombieClone.transform.SetParent(ZombieContainer);
        currentZombiesInScene++;
        currentZombiesPerRound++;
    }

    private int SelectOneSpawn()
    {
        SelectWhatSpawnCanSpawnZombies();
        return Random.Range(0, spawnsActived.Count);
    }

    public void ResetAllCurrentZombies()
    {
        currentZombiesInScene = 0;
        currentZombiesPerRound = 0;
    }

    private void SelectWhatSpawnCanSpawnZombies()
    {
        spawnsActived.Clear();
        foreach (GameObject spawn in spawns)
        {
            if (CheckDistanceToPlayer(spawn))
            {
                spawnsActived.Add(spawn);
            }
        }
    }

    private bool CheckDistanceToPlayer(GameObject spawn)
    {
        if (Vector3.Distance(spawn.transform.position, _playerMovement.transform.position) < maxDistanceToPlayerToCanSpawn)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        foreach (GameObject spawn in spawns)
        {
            if (Vector3.Distance(spawn.transform.position, _playerMovement.transform.position) < maxDistanceToPlayerToCanSpawn)
            {
                Gizmos.color = Color.blue;
            }
            else
            {
                Gizmos.color = Color.red;
            }
            Gizmos.DrawLine(spawn.transform.position, _playerMovement.transform.position);
        }
    }

}
