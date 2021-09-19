using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Perk : MonoBehaviour
{
    [SerializeField] private ScriptablePerk scriptablePerk;

    private Text _perkText;
    private bool _canBuy;
    private PlayerPerkManager _playerPerkManager;
    private PowerOn powerOn;

    private void Awake()
    {
        _playerPerkManager = FindObjectOfType<PlayerPerkManager>();
        _perkText = GameObject.FindGameObjectWithTag("PerkText").GetComponent<Text>();
        powerOn = FindObjectOfType<PowerOn>();
    }

    private void Start()
    {
        _perkText.enabled = false;
        powerOn.PowerOnReleased += TurnOnPerk;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_canBuy)
            {
                UpdatePerkText();
                _perkText.enabled = true;
            }
            else
            {
                _perkText.text = "Power is required";
                _perkText.enabled = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_canBuy)
            {
                _perkText.enabled = true;
                if (Input.GetKeyDown(KeyCode.F))
                {
                    PlayerScore playerScore = other.GetComponent<PlayerScore>();
                    if (playerScore.score >= scriptablePerk.cost)
                    {
                        _perkText.enabled = false;
                        _canBuy = false;
                        playerScore.QuitScore(scriptablePerk.cost);
                        _playerPerkManager.SelectPerk(scriptablePerk.perkType, scriptablePerk.perkIcon);
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _perkText.enabled = false;
        }
    }

    private void TurnOnPerk()
    {
        _canBuy = true;
    }

    private void UpdatePerkText()
    {
        _perkText.text = "Press F to buy " + scriptablePerk.name + " (" + scriptablePerk.cost + ")" + "\n[" + scriptablePerk.description + "]";
    }
}
