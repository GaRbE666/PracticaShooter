using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] private float timeToDisappear;
    public ItemScriptable itemScriptable;
    private bool itemCatched;
    private GameObject itemNameText;

    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;
    private Text timer;
    private Text timerToD;
    private float timeToDestroy;

    private void Awake()
    {
        itemNameText = GameObject.FindGameObjectWithTag("ItemText");
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        timer = GameObject.Find("Timer").GetComponent<Text>();
        timerToD = GameObject.Find("Timer2").GetComponent<Text>();
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
            timer.text = timeToDestroy.ToString("00");

        }
        else
        {
            timeToDisappear -= Time.deltaTime;
            timerToD.text = timeToDisappear.ToString("00");
            if (timeToDisappear <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            itemCatched = true;
            meshRenderer.enabled = false;
            boxCollider.enabled = false;
            StartCoroutine(ShowTextCoroutine());
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
            Debug.Log(enemy.GetComponent<EnemyHealth>().currentHealth);
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
            enemy.GetComponent<EnemyIA>().pointsReward *= 2;
        }

        yield return new WaitForSeconds(itemScriptable.timeToDestroy);

        foreach (EnemyIA enemy in enemies)
        {
            enemy.GetComponent<EnemyIA>().pointsReward /= 2;
        }

        Destroy(gameObject);
    }

    private void Cash(Collider other)
    {
        other.GetComponent<PlayerScore>().AddScore(1000);
        Destroy(gameObject);
    }

    private IEnumerator ShowTextCoroutine()
    {
        Debug.Log("Enciendo Texto");
        itemNameText.GetComponentInChildren<Text>().text = itemScriptable.itemName;
        itemNameText.gameObject.transform.GetChild(0).GetComponentInChildren<Text>().enabled = true;
        yield return new WaitForSeconds(4f);
        Debug.Log("Apago texto");
        itemNameText.gameObject.transform.GetChild(0).GetComponentInChildren<Text>().enabled = false;
    }

}
