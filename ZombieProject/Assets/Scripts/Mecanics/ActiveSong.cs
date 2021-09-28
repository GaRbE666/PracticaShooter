using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSong : MonoBehaviour
{
    private SongManager _songManager;

    private void Awake()
    {
        _songManager = FindObjectOfType<SongManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                GetComponent<BoxCollider>().enabled = false;
                GetComponent<AudioSource>().Stop();
                _songManager.partActived++;
                _songManager.PlayAudioPart();
            }
        }
    }
}
