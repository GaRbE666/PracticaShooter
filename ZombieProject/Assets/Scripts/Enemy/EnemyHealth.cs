using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private Transform itemSpawn;
    public float currentHealth;
    public EnemyScriptable zScriptable;
    public bool die;
    [SerializeField] private EnemyAnimations enemyAnimation;

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
        BoxCollider[] boxColliders = GetComponentsInChildren<BoxCollider>();
        foreach (BoxCollider collider in boxColliders)
        {
            collider.enabled = false;
        }
        DropRandomItem();
        Destroy(gameObject, 30f);
    }

    private void DropRandomItem()
    {
        int numRand = Random.Range(0, 7);
        if (Mathf.Clamp(numRand, 0, 2) >= 0 || Mathf.Clamp(numRand, 0, 2) <= 2)
        {
            int indexRand = Random.Range(0, zScriptable.items.Count);
            Instantiate(zScriptable.items[indexRand], itemSpawn.position, itemSpawn.rotation);
        }
    }
}
