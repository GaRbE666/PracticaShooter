using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] spawns;
    [SerializeField] private GameObject zombie;
    [SerializeField] private Transform ZombieContainer;

    [Header("Parameters")]
    public int maxZombiesPerRound;
    [SerializeField] private int maxZombiesInScene;
    [SerializeField] private float maxTimeToSpawn;
    [SerializeField] private float minTimeToSpawn;

    private bool _canSpawn;
    private bool _canSpawnMore;
    private float _timeRandomToSpawn;
    [HideInInspector] public bool _endRound;
    public int currentZombiesInScene;
    public int currentZombiesPerRound;

    private void Start()
    {
        _canSpawnMore = true;
        _canSpawn = true;
    }

    private void Update()
    {
        CheckNumberMaxOfZombiesInScene();
        CheckMaxZombiesPerRound();
        if (_canSpawn && _canSpawnMore && !_endRound)
        {
            StartCoroutine(SpawnZombie());
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

    private IEnumerator SpawnZombie()
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
        GameObject zombieClone = Instantiate(zombie, spawns[spawnRandom].transform.position, spawns[spawnRandom].transform.rotation);
        zombieClone.transform.SetParent(ZombieContainer);
        currentZombiesInScene++;
        currentZombiesPerRound++;
    }

    private int SelectOneSpawn()
    {
        return Random.Range(0, spawns.Length);

    }

    public void ResetAllCurrentZombies()
    {
        currentZombiesInScene = 0;
        currentZombiesPerRound = 0;
    }

}
