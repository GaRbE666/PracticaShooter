using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Text teleportText;
    private GameManager _gameManager;
    private TeleportSpawn _teleportSpawn;
    public bool teleportActived;
    public bool link1;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _teleportSpawn = FindObjectOfType<TeleportSpawn>();
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
            if (_teleportSpawn.link2)
            {
                SetTeleportText("Press F to Teleport");
            }
            else
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
                    ActiveText();
                }
                else
                {
                    SetTeleportText("Link is required");
                    ActiveText();
                }

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

    }
}
