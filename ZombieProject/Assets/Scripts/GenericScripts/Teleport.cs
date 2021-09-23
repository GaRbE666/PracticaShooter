using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Text teleportText;
    [SerializeField] private Transform teleportDestino;
    private GameManager _gameManager;
    private TeleportSpawn _teleportSpawn;
    private PlayerMovement _playerMovement;
    public bool teleportActived;
    public bool link1;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _teleportSpawn = FindObjectOfType<TeleportSpawn>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Start()
    {
        teleportText.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (CheckPowerIsOn() && !_teleportSpawn.link2)
            {
                SetTeleportText("Press F to activate link");
            }
            if (teleportActived)
            {
                SetTeleportText("Press F to Teleport");
            }
            if(!CheckPowerIsOn())
            {
                SetTeleportText("Power is required");
            }
            ActiveText();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (CheckPowerIsOn())
            {
                if (!link1)
                {
                    SetTeleportText("Press F to activate link");
                }
                if (teleportActived)
                {
                    SetTeleportText("Press F to Teleport");
                }
                else
                {
                    SetTeleportText("Link is required");
                    
                }
                ActiveText();
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (!_teleportSpawn.link2)
                    {
                        LinkUp();
                    }
                    else
                    {
                        TeleportToPAP();
                    } 
                }
            }
            else
            {
                SetTeleportText("Power is required");
                ActiveText();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DesactiveText();
        }
    }

    private void ActiveText()
    {
        teleportText.enabled = true;
    }

    private void DesactiveText()
    {
        teleportText.enabled = false;
    }

    private bool CheckPowerIsOn()
    {
        return _gameManager.powerOn;
    }

    private void LinkUp()
    {
        link1 = true;
        //teleportActived = true;
    }

    private void SetTeleportText(string text)
    {
        teleportText.text = text;
    }

    private void TeleportToPAP()
    {
        _playerMovement.gameObject.transform.root.transform.position = teleportDestino.position;
    }
}
