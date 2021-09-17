using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Perk : MonoBehaviour
{
    [SerializeField] private string perkName;
    [SerializeField] private int cost;
    public enum TypeOfPerks { QuickRevive, Juggernaut, SpeedCola, DoubleTap, StamminUp};
    public TypeOfPerks perkType;

    private Text perkText;
    private bool canBuy = true;
    private PlayerPerkManager playerPerkManager;

    private void Awake()
    {
        playerPerkManager = FindObjectOfType<PlayerPerkManager>();
        perkText = GameObject.FindGameObjectWithTag("PerkText").GetComponent<Text>();
        gameObject.transform.parent.name = perkName;
    }

    private void Start()
    {
        perkText.enabled = false;
        UpdatePerkText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (canBuy)
            {
                perkText.enabled = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (canBuy)
            {
                perkText.enabled = true;
                if (Input.GetKeyDown(KeyCode.F))
                {
                    PlayerScore playerScore = other.GetComponent<PlayerScore>();
                    if (playerScore.score >= cost)
                    {
                        perkText.enabled = false;
                        canBuy = false;
                        playerScore.QuitScore(cost);
                        playerPerkManager.SelectPerk(perkType, gameObject.transform.parent.gameObject);
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            perkText.enabled = false;
        }
    }

    private void UpdatePerkText()
    {
        perkText.text = "Press F to buy " + perkName + " (" + cost + ")";
    }
}
