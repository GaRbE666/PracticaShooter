using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportSpawn : MonoBehaviour
{
    [SerializeField] private Text teleportSpawnText;
    [SerializeField] private GameObject cableLink;
    [SerializeField] private Material cableLinkMaterialOn;
    [SerializeField] private Material cableLinkMaterialOff;
    public bool link2;
    private Teleport _teleport;
    private GameManager _gameManager;
    private PlayerAudio _playerAudio;

    private void Awake()
    {
        _teleport = FindObjectOfType<Teleport>();
        _gameManager = FindObjectOfType<GameManager>();
        _playerAudio = FindObjectOfType<PlayerAudio>();
    }

    private void Start()
    {
        teleportSpawnText.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_gameManager.powerOn)
            {
                if (_teleport.link1 && !link2)
                {
                    SetTeleportSpawnText("Presiona F para iniciar la conexión con la plataforma");
                }
                else
                {
                    SetTeleportSpawnText("Esperando enlace");
                }
            }
            else
            {
                _playerAudio.PlayNoPowerAudio();
                SetTeleportSpawnText("Antes debes activar la corriente");
            }
            ActiveText();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ChooseIfShowText();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DesactiveText();
        }
    }

    private void ChooseIfShowText()
    {
        if (_teleport.link1 && !link2)
        {
            SetTeleportSpawnText("Presiona F para enlazar la plataforma con el nucleo");
            ActiveText();
            PlayerPressKey();
        }
    }

    private void ActiveText()
    {
        teleportSpawnText.enabled = true;
    }

    private void PlayerPressKey()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            DesactiveText();
            if (_teleport.link1)
            {
                LinkUp();
            }
        }
    }

    private void DesactiveText()
    {
        teleportSpawnText.enabled = false;
    }

    private void SetTeleportSpawnText(string text)
    {
        teleportSpawnText.text = text;
    }

    private void LinkUp()
    {
        _teleport.teleportActived = true;
        link2 = true;
        PutCableMaterialOn();
    }

    public void PutCableMaterialOn()
    {
        cableLink.GetComponent<MeshRenderer>().material = cableLinkMaterialOn;
    }

    public void PutCableMaterialOff()
    {
        cableLink.GetComponent<MeshRenderer>().material = cableLinkMaterialOff;
    }
}
