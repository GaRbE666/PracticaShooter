using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private Transform itemSpawn;
    [SerializeField] private EnemyAnimations enemyAnimation;
    public float currentHealth;
    public EnemyScriptable zScriptable;
    public bool die;
  
    private PlayerScore playerScore;
    private EnemyIA enemyIA;

    private void Awake()
    {
        playerScore = FindObjectOfType<PlayerScore>();
        enemyIA = GetComponent<EnemyIA>();
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
            Die();
        }
    }

    private void Die()
    {
        die = true;
        DisableAllColliders();
        DropRandomItem();
        AddScoreToPlayer();
        Destroy(gameObject, 30f);
    }

    private void AddScoreToPlayer()
    {
        playerScore.AddScore(enemyIA.pointsReward);
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
        int numRand = Random.Range(0, 7);
        Debug.Log(numRand);
        if (numRand == 1)
        {
            int indexRand = Random.Range(0, zScriptable.items.Count);
            Instantiate(zScriptable.items[indexRand], itemSpawn.position, itemSpawn.rotation);
        }
    }
}
