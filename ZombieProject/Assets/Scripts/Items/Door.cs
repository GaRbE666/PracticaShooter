using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public int pointsRequired;
    [SerializeField] private Text doorText;
    [SerializeField] private GameObject[] doors;
    [SerializeField] private bool electricityRequired;

    private PowerOn _powerOn;
    private PlayerAudio _playerAudio;

    private void Awake()
    {
        _powerOn = FindObjectOfType<PowerOn>();
        _playerAudio = FindObjectOfType<PlayerAudio>();
    }

    private void Start()
    {
        _powerOn.PowerOnReleased += DisablePowerDoor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorText.enabled = true;
            ChangeTextValue();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPressKey(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorText.enabled = false;
        }
    }

    private void PlayerPressKey(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            CheckIfPowerIsRequired(other);
        }
    }

    private void CheckIfPowerIsRequired(Collider other)
    {
        if (!electricityRequired)
        {
            CheckPlayerScore(other);
        }
    }

    private void CheckPlayerScore(Collider other)
    {
        PlayerScore playerScore = other.GetComponent<PlayerScore>();
        if (playerScore.score >= pointsRequired)
        {
            doorText.enabled = false;
            playerScore.QuitScore(pointsRequired);
            DisableAllDoors();
        }
        else
        {
            _playerAudio.PlayNoMoneyAudio();
        }
    }

    private void ChangeTextValue()
    {
        if (electricityRequired)
        {
            doorText.text = "Antes debes activar la corriente";
        }
        else
        {
            doorText.text = "Mantén F para abrir la puerta (coste: " + pointsRequired + ")";
        }
        
    }

    private void DisableAllDoors()
    {
        foreach (GameObject door in doors)
        {
            door.SetActive(false);
        }
    }

    private void DisablePowerDoor()
    {
        if (electricityRequired)
        {
            foreach (GameObject door in doors)
            {
                door.SetActive(false);
            }
        }
    }
}
