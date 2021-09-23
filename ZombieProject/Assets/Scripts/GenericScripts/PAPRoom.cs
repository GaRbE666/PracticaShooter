using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAPRoom : MonoBehaviour
{
    [SerializeField] private float timeToExtiRoom;
    [SerializeField] private Transform spawnToMainRoom;
    private Teleport _teleport;
    private TeleportSpawn _teleportSpawn;
    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _teleport = FindObjectOfType<Teleport>();
        _teleportSpawn = FindObjectOfType<TeleportSpawn>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
    }

    public IEnumerator CoolDownToExitRoom()
    {
        yield return new WaitForSeconds(timeToExtiRoom);
        ResetTwoTeleportStations();
        ExitRoom();
    }

    public void ExitRoom()
    {
        TeleportPlayer();
    }

    private void ResetTwoTeleportStations()
    {
        _teleport.teleportActived = false;
        _teleport.link1 = false;
        _teleportSpawn.link2 = false;
        _teleport.teleportIsRecovering = true;
        _teleportSpawn.PutCableMaterialOff();
        StartCoroutine(_teleport.TimeToReActiveTeleport());
    }

    private void TeleportPlayer()
    {
        _playerMovement.GetComponent<CharacterController>().enabled = false;
        _playerMovement.transform.position = spawnToMainRoom.position;
        _playerMovement.transform.rotation = spawnToMainRoom.rotation;
        _playerMovement.GetComponent<CharacterController>().enabled = true;
    }
}
