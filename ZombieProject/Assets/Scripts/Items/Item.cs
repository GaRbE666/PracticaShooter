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
    public bool instakillActived;
    public bool doublePointsActived;

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
                Destroy(gameObject, 1f);
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
                instakillActived = true;
                transform.name = "InstakillActived";
                StartCoroutine(InstaKill());
                break;
            case ItemScriptable.itemEnumType.DoublePoints:
                doublePointsActived = true;
                transform.name = "DoublePointsActived";
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
        yield return new WaitForSeconds(itemScriptable.timeToDestroy);
        instakillActived = false;
        EnemyHealth[] enemies2 = FindObjectsOfType<EnemyHealth>();
        foreach (EnemyHealth enemy in enemies2)
        {
            if (!enemy.GetComponent<EnemyHealth>().die)
            {
                enemy.GetComponent<EnemyHealth>().currentHealth = enemy.GetComponent<EnemyHealth>().zScriptable.maxHealth;
            }
            Debug.Log(enemy.GetComponent<EnemyHealth>().currentHealth);
        }
    }

    private IEnumerator DoublePoints()
    {
        yield return new WaitForSeconds(itemScriptable.timeToDestroy);

        EnemyIA[] enemies2 = FindObjectsOfType<EnemyIA>();
        doublePointsActived = false;
        Debug.Log("Descativo los dobles puntos");
        foreach (EnemyIA enemy in enemies2)
        {
            if (enemy != null && !enemy.GetComponent<EnemyHealth>().die)
            {
                enemy.GetComponent<EnemyIA>().pointsReward /= 2;
                enemy.GetComponent<EnemyIA>().pointsForHitReward /= 2;
            }
        }
    }

    private void Cash(Collider other)
    {
        other.GetComponent<PlayerScore>().AddScore(1000);
        Destroy(gameObject);
    }

}
