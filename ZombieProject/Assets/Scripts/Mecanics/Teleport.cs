using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Text teleportText;
    [SerializeField] private Transform teleportDestino;
    [SerializeField] private Transform spawnMainRoom;
    [SerializeField] private float timeToReactiveTeleport;
    [SerializeField] private float timeToExitPAPRoom;
    private GameManager _gameManager;
    private TeleportSpawn _teleportSpawn;
    private PlayerMovement _playerMovement;
    //private PAPRoom _papRoom;
    public bool teleportActived;
    public bool link1;
    public bool teleportIsRecovering;

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

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        if (CheckPowerIsOn() && !_teleportSpawn.link2)
    //        {
    //            SetTeleportText("Press F to activate link");
    //        }
    //        if (teleportActived)
    //        {
    //            SetTeleportText("Press F to Teleport");
    //        }
    //        if(!CheckPowerIsOn())
    //        {
    //            SetTeleportText("Power is required");
    //        }
    //        ActiveText();
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (CheckPowerIsOn())
            {
                ChooseWhatTextShowOnStay();
                PlayerPressKey();
            }
            else
            {
                SetTeleportText("Antes debes activar la corriente");
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

    private void PlayerPressKey()
    {
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

    private void ChooseWhatTextShowOnStay()
    {
        if (!teleportIsRecovering)
        {
            if (!link1)
            {
                SetTeleportText("Mantén F para iniciar la conexion con la plataforma");
            }
            else if (link1 && !_teleportSpawn.link2)
            {
                SetTeleportText("Enlace inactivo");
            }
            else if (teleportActived)
            {
                SetTeleportText("Mantén F para usar la plataforma de teletransporte");
            }
        }
        else
        {
            SetTeleportText("La plataforma se está recuperando");
        }
        ActiveText();
    }

    public IEnumerator TimeToReActiveTeleport()
    {
        yield return new WaitForSeconds(timeToReactiveTeleport);
        teleportIsRecovering = false;
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
    }

    private void SetTeleportText(string text)
    {
        teleportText.text = text;
    }

    private void TeleportToPAP()
    {
        TeleportPlayerToPAP();
        StartCoroutine(CoolDownExitPAPRoom());
    }

    private IEnumerator CoolDownExitPAPRoom()
    {
        yield return new WaitForSeconds(timeToExitPAPRoom);
        ResetTwoTeleportStations();
        TeleportPlayerToMainRoom();
    }


    private void ResetTwoTeleportStations()
    {
        teleportActived = false;
        link1 = false;
        _teleportSpawn.link2 = false;
        teleportIsRecovering = true;
        _teleportSpawn.PutCableMaterialOff();
        StartCoroutine(TimeToReActiveTeleport());
    }

    private void TeleportPlayerToMainRoom()
    {
        _gameManager.playerTeleported = false;
        _playerMovement.GetComponent<CharacterController>().enabled = false;
        _playerMovement.transform.position = spawnMainRoom.position;
        _playerMovement.transform.rotation = spawnMainRoom.rotation;
        _playerMovement.GetComponent<CharacterController>().enabled = true;
    }

    private void TeleportPlayerToPAP()
    {
        DesactiveText();
        _gameManager.playerTeleported = true;
        _playerMovement.GetComponent<CharacterController>().enabled = false;
        _playerMovement.transform.position = teleportDestino.position;
        _playerMovement.transform.rotation = teleportDestino.rotation;
        _playerMovement.GetComponent<CharacterController>().enabled = true;
    }
}
