using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [Header("Round Sounds")]
    [SerializeField] private AudioClip roundEnd;
    [SerializeField] private AudioClip roundStart;


    public void PlayEndRoundSound()
    {
        audioSource.PlayOneShot(roundEnd);
    }

    public void PlayStartRound()
    {
        audioSource.PlayOneShot(roundStart);
    }

}
