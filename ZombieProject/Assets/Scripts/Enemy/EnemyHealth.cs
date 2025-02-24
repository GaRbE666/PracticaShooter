﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private Transform itemSpawn;
    [SerializeField] private EnemyAnimations enemyAnimation;
    [SerializeField] private bool alwaysDroopItem;
    [SerializeField] private GameObject enemyHeadReference;
    public float currentHealth;
    public EnemyScriptable zScriptable;
    public bool die;
  
    private PlayerScore _playerScore;
    private EnemyIA _enemyIA;
    private SpawnManager _spawnManager;
    private EnemyAudio _enemyAudio;
    private PlayerAudio _playerAudio;
    private GameManager _gameManager;

    private void Awake()
    {
        _playerScore = FindObjectOfType<PlayerScore>();
        _enemyIA = GetComponent<EnemyIA>();
        _spawnManager = FindObjectOfType<SpawnManager>();
        _enemyAudio = GetComponentInChildren<EnemyAudio>();
        _playerAudio = FindObjectOfType<PlayerAudio>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = zScriptable.maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
        else
        {
            AddSomeScoreToPlayer();
        }
        
    }

    public IEnumerator Die()
    {
        DisableAllColliders();
        yield return new WaitForSeconds(.2f);
        _playerAudio.PlayKillZombieAudio();
        die = true;
        _spawnManager.currentZombiesInScene--;
        _gameManager.totalZombiesKilled++;
        DropRandomItem();
        AddScoreToPlayer();
        Destroy(gameObject, 30f);
    }

    private void AddSomeScoreToPlayer()
    {
        _playerScore.AddScore(_enemyIA.pointsForHitReward);
    }

    private void AddScoreToPlayer()
    {
        _playerScore.AddScore(_enemyIA.pointsReward);
    }

    private void DisableAllColliders()
    {
        BoxCollider[] boxColliders = GetComponentsInChildren<BoxCollider>();
        foreach (BoxCollider collider in boxColliders)
        {
            collider.enabled = false;
        }
    }

    private void DropRandomItem()
    {
        if (alwaysDroopItem)
        {
            int indexRand = Random.Range(0, zScriptable.items.Count);
            Instantiate(zScriptable.items[indexRand], itemSpawn.position, itemSpawn.rotation);
        }
        else
        {
            int numRand = Random.Range(0, 100);

            if (numRand <= zScriptable.dropPercent)
            {
                int indexRand = Random.Range(0, zScriptable.items.Count);
                Instantiate(zScriptable.items[indexRand], itemSpawn.position, itemSpawn.rotation);
            }
        }

    }

    public void DisableHead()
    {
        enemyHeadReference.SetActive(false);
        _enemyAudio.PlayHeadShoot();
        _gameManager.headShootCount++;
    }
}
