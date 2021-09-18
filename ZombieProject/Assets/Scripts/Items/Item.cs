using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] private float timeToDisappear;
    public ItemScriptable itemScriptable;

    private bool itemCatched;

    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;
    private Text powerUpTimerEffect;
    private float timeToDestroy;
    private PlayerItemManager playerItemManager;

    private void Awake()
    {
        playerItemManager = FindObjectOfType<PlayerItemManager>();
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        powerUpTimerEffect = FindObjectOfType<PowerUpTimer>().GetComponent<Text>();
    }

    private void Start()
    {
        timeToDestroy = itemScriptable.timeToDestroy;
    }

    private void Update()
    {
        if (itemCatched)
        {
            timeToDestroy -= Time.deltaTime;
            UpdatePowerUpText();
            if (timeToDestroy <= 0)
            {
                Destroy(gameObject);
            }

        }
        else
        {
            timeToDisappear -= Time.deltaTime;
            
            if (timeToDisappear <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void UpdatePowerUpText()
    {
        powerUpTimerEffect.enabled = true;
        powerUpTimerEffect.text = timeToDestroy.ToString("00");
        if (timeToDestroy <= 0)
        {
            powerUpTimerEffect.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            itemCatched = true;
            meshRenderer.enabled = false;
            boxCollider.enabled = false;
            playerItemManager.ShowText(itemScriptable.name);
            SelectItem(other);
            
        }
    }

    private void SelectItem(Collider other)
    {
        switch (itemScriptable.itemType)
        {
            case ItemScriptable.itemEnumType.MaxAmmo:
                MaxAmmo(other);
                break;
            case ItemScriptable.itemEnumType.InstaKill:
                StartCoroutine(InstaKill());
                break;
            case ItemScriptable.itemEnumType.DoublePoints:
                StartCoroutine(DoublePoints());
                break;
            case ItemScriptable.itemEnumType.Cash:
                Cash(other);
                break;
            case ItemScriptable.itemEnumType.Kaboom:
                Kaboom();
                break;

        }
    }

    private void Kaboom()
    {
        EnemyHealth[] enemies = FindObjectsOfType<EnemyHealth>();
        List<EnemyHealth> enemiesAlive = new List<EnemyHealth>();

        foreach (EnemyHealth enemy in enemies)
        {
            if (!enemy.die)
            {
                enemiesAlive.Add(enemy);
            }
        }

        foreach (EnemyHealth enemy in enemiesAlive)
        {
            enemy.Die();
        }
    }

    private void MaxAmmo(Collider other)
    {
        other.GetComponent<PlayerItemManager>().MaxAmmo();
    }

    private IEnumerator InstaKill()
    {
        EnemyHealth[] enemies = FindObjectsOfType<EnemyHealth>();
        foreach (EnemyHealth enemy in enemies)
        {
            enemy.GetComponent<EnemyHealth>().currentHealth = 1;
        }

        yield return new WaitForSeconds(itemScriptable.timeToDestroy);

        foreach (EnemyHealth enemy in enemies)
        {
            Debug.Log("Quito el instakill");
            enemy.GetComponent<EnemyHealth>().currentHealth = enemy.GetComponent<EnemyHealth>().zScriptable.maxHealth;
            Debug.Log(enemy.GetComponent<EnemyHealth>().currentHealth);
        }

        Destroy(gameObject);
    }

    private IEnumerator DoublePoints()
    {
        EnemyIA[] enemies = FindObjectsOfType<EnemyIA>();
        foreach (EnemyIA enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.GetComponent<EnemyIA>().pointsReward *= 2;
            }
        }

        yield return new WaitForSeconds(itemScriptable.timeToDestroy);

        foreach (EnemyIA enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.GetComponent<EnemyIA>().pointsReward /= 2;
            }
        }

        Destroy(gameObject);
    }

    private void Cash(Collider other)
    {
        other.GetComponent<PlayerScore>().AddScore(1000);
        Destroy(gameObject);
    }

}
