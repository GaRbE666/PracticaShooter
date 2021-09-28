using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip perkSong;

    private bool CanPlayOneAudio(int probability) //Metodo que determina en base a una probabilidad dada (entre 0 y 100) si un audio se reproduce o no
    {
        int numRand = Random.Range(0, 100);

        if (numRand <= probability && !audioSource.isPlaying)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void PlayPerkSongSometimes()
    {
        if (CanPlayOneAudio(30))
        {
            audioSource.PlayOneShot(perkSong);
        }
    }

    public void PlayPerkSongSecure()
    {
        if (CanPlayOneAudio(100))
        {
            audioSource.PlayOneShot(perkSong);
        }
    }
}
