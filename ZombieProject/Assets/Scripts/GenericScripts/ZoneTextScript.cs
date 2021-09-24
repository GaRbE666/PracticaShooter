using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoneTextScript : MonoBehaviour
{
    [SerializeField] private Text zoneText;
    [SerializeField] private string zoneName;

    private SpawnManager _spawnManager;
    private GameObject[] _spawns;

    private void Awake()
    {
        _spawnManager = FindObjectOfType<SpawnManager>();
    }

    private void StorageAllSpawnsInRoom()
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            if (_spawnManager.spawns.Contains(transform.GetChild(i).gameObject))
            {
                return;
            }
            else
            {
                _spawnManager.spawns.Add(transform.GetChild(i).gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StorageAllSpawnsInRoom();
            zoneText.text = zoneName;
            zoneText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            zoneText.text = zoneName;
            zoneText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            zoneText.gameObject.SetActive(false);
        }
    }


}
