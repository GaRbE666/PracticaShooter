using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Teleport : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Text teleportText;
    [SerializeField] private Transform teleportDestino;
    [SerializeField] private Transform spawnMainRoom;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioSourcePAP;
    [SerializeField] private AudioClip linkActived;
    [SerializeField] private AudioClip teleportSend1;
    [SerializeField] private AudioClip teleportSend2;
    [SerializeField] private VideoPlayer teleportVideo;
    [Header("Parameters")]
    [SerializeField] private float timeToReactiveTeleport;
    [SerializeField] private float timeToExitPAPRoom;
    private GameManager _gameManager;
    private TeleportSpawn _teleportSpawn;
    private PlayerMovement _playerMovement;
    private PlayerAudio _playerAudio;
    //private PAPRoom _papRoom;
    [HideInInspector] public bool teleportActived;
    [HideInInspector] public bool link1;
    [HideInInspector] public bool teleportIsRecovering;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _teleportSpawn = FindObjectOfType<TeleportSpawn>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _playerAudio = FindObjectOfType<PlayerAudio>();
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
                _playerAudio.PlayNoPowerAudio();
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
        audioSource.PlayOneShot(linkActived);
        link1 = true;
    }

    private void SetTeleportText(string text)
    {
        teleportText.text = text;
    }

    private void TeleportToPAP()
    {
        StartCoroutine(TeleportPlayerToPAP());
        StartCoroutine(CoolDownExitPAPRoom());
    }

    private IEnumerator CoolDownExitPAPRoom()
    {
        yield return new WaitForSeconds(timeToExitPAPRoom);
        ResetTwoTeleportStations();
        StartCoroutine(TeleportPlayerToMainRoom());
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

    private IEnumerator TeleportPlayerToMainRoom()
    {
        _gameManager.playerTeleported = false;
        StartCoroutine(ActivateSoundsPAPTeleport());
        yield return new WaitForSeconds(6f);
        ChangePlayerPositionToMainRoom();
        _playerMovement.UnlockPlayer();
        teleportVideo.Stop();
    }

    private IEnumerator TeleportPlayerToPAP()
    {
        teleportText.gameObject.SetActive(false);
        StartCoroutine(ActiveSoundsTeleport());
        _playerMovement.LockPlayer();
        yield return new WaitForSeconds(6f);
        _gameManager.playerTeleported = true;
        ChangePlayerPositionToPAP();
        _playerMovement.UnlockPlayer();
        teleportVideo.Stop();
        teleportText.gameObject.SetActive(true);

    }

    private void ChangePlayerPositionToMainRoom()
    {
        _playerMovement.GetComponent<CharacterController>().enabled = false;
        _playerMovement.transform.position = spawnMainRoom.position;
        _playerMovement.transform.rotation = spawnMainRoom.rotation;
        _playerMovement.GetComponent<CharacterController>().enabled = true;
    }

    private void ChangePlayerPositionToPAP()
    {
        _playerMovement.GetComponent<CharacterController>().enabled = false;
        _playerMovement.transform.position = teleportDestino.position;
        _playerMovement.transform.rotation = teleportDestino.rotation;
        _playerMovement.GetComponent<CharacterController>().enabled = true;
    }

    private IEnumerator ActivateSoundsPAPTeleport()
    {
        audioSourcePAP.PlayOneShot(teleportSend1);
        yield return new WaitForSeconds(3f);
        _playerMovement.LockPlayer();
        teleportVideo.Play();
        audioSourcePAP.PlayOneShot(teleportSend2);
    }

    private IEnumerator ActiveSoundsTeleport()
    {
        audioSource.PlayOneShot(teleportSend1);
        yield return new WaitForSeconds(3f);
        teleportVideo.Play();
        audioSource.PlayOneShot(teleportSend2);
    }
}
