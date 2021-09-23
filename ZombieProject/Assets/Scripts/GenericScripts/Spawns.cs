using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawns : MonoBehaviour
{
    private SpawnManager spawnManager;

    private void Awake()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

        }
    }
}
