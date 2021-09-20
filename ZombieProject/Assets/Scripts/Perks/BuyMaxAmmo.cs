using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyMaxAmmo : MonoBehaviour
{
    [SerializeField] private Text perkText;
    [SerializeField] private int cost;

    private PlayerItemManager playerItemManager;
    private PlayerScore playerScore;

    private void Awake()
    {
        playerItemManager = FindObjectOfType<PlayerItemManager>();
        playerScore = FindObjectOfType<PlayerScore>();
    }

    private void Start()
    {
        UpdateText();
        perkText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            perkText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            perkText.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (playerScore.score >= cost)
                {
                    playerScore.QuitScore(cost);
                    playerItemManager.MaxAmmo();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            perkText.gameObject.SetActive(false);
        }
    }

    private void UpdateText()
    {
        perkText.text = "Press F to Max Ammo " + cost.ToString(); ;
    }
}
