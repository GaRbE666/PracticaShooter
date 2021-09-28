using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Perk : MonoBehaviour
{
    [SerializeField] private ScriptablePerk scriptablePerk;
    [SerializeField] private PerkAudio perkAudio;

    [SerializeField] private Text _perkText;
    private bool _canBuy;
    private PlayerPerkManager _playerPerkManager;
    private PowerOn _powerOn;
    private PlayerAudio _playerAudio;
    [HideInInspector] public bool perkAdquired;

    private void Awake()
    {
        _playerPerkManager = FindObjectOfType<PlayerPerkManager>();
        //_perkText = GameObject.FindGameObjectWithTag("PerkText").GetComponent<Text>();
        _powerOn = FindObjectOfType<PowerOn>();
        _playerAudio = FindObjectOfType<PlayerAudio>();
    }

    private void Start()
    {
        _perkText.enabled = false;
        _powerOn.PowerOnReleased += TurnOnPerk;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_canBuy)
            {
                UpdatePerkText();
                _perkText.enabled = true;
                perkAudio.PlayPerkSongSometimes();
            }else if (CheckIfPerkAdquired(scriptablePerk.perkType))
            {
                perkAudio.PlayPerkSongSometimes();
                _perkText.enabled = false;
            }
            else
            {
                _playerAudio.PlayNoPowerAudio();
                _perkText.text = "Antes debes activar la corriente";
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
                    if (!CheckIfPerkAdquired(scriptablePerk.perkType))
                    {
                        ConsumePerk(other);
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

    private void ConsumePerk(Collider other)
    {
        PlayerScore playerScore = other.GetComponent<PlayerScore>();
        if (playerScore.score >= scriptablePerk.cost)
        {
            perkAudio.PlayPerkSongSecure();
            _perkText.enabled = false;
            _canBuy = false;
            playerScore.QuitScore(scriptablePerk.cost);
            _playerPerkManager.SelectPerk(scriptablePerk.perkType, scriptablePerk.perkIcon);
        }
        else
        {
            _playerAudio.PlayNoMoneyAudio();
        }
    }

    private bool CheckIfPerkAdquired(ScriptablePerk.TypeOfPerks perkType)
    {
        switch (perkType)
        {
            case ScriptablePerk.TypeOfPerks.QuickRevive:
                return _playerPerkManager.quickAdquired;
            case ScriptablePerk.TypeOfPerks.Juggernaut:
                return _playerPerkManager.juggerAdquired;
            case ScriptablePerk.TypeOfPerks.SpeedCola:
                return _playerPerkManager.speedColaAdquired;
            case ScriptablePerk.TypeOfPerks.DoubleTap:
                return _playerPerkManager.doubleTapAdquired;
            case ScriptablePerk.TypeOfPerks.StamminUp:
                return _playerPerkManager.stamminUpAdquired;
            default:
                return false;
        }
    }

    private void TurnOnPerk()
    {
        _canBuy = true;
    }

    private void UpdatePerkText()
    {
        _perkText.text = "Mantén F para comprar " + scriptablePerk.name + " (" + scriptablePerk.cost + ")" + "\n[" + scriptablePerk.description + "]";
    }
}
