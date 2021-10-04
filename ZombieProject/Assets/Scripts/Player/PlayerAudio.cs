using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioSourceSteps;
    [Header("KillZombie Audios")]
    [SerializeField] private AudioClip[] killZombie;
    [Header("HeadShoot Audios")]
    [SerializeField] private AudioClip[] headShoot;
    [Header("ReceiveDamage Audios")]
    [SerializeField] private AudioClip[] playerDamage;
    [Header("No money Audios")]
    [SerializeField] private AudioClip[] noMoney;
    [Header("Ammo Audios")]
    [SerializeField] private AudioClip lowAmmo;
    [SerializeField] private AudioClip[] noAmmo;
    [Header("Power Audios")]
    [SerializeField] private AudioClip[] noPower;
    [SerializeField] private AudioClip power;
    [Header("Song Parts Audio")]
    [SerializeField] private AudioClip[] songParts;
    [Header("PowerUps Audios")]
    [SerializeField] private AudioClip instaKill;
    [SerializeField] private AudioClip doublePoints;
    [SerializeField] private AudioClip kaboom;
    [SerializeField] private AudioClip maxAmmo;
    [Header("Steps Audios")]
    [SerializeField] private AudioClip[] steps;
    [Header("Death Sound")]
    [SerializeField] private AudioClip death;

    public bool _noAmmoPlaying;
    public bool _lowAmmoPlaying;
  
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

    private int SelectOneAudioRandom(AudioClip[] audioList) //Método que devuelve un audio aleatorio dentro de una lista de audios
    {
        return Random.Range(0, audioList.Length);
    }

    public void PlayDeathSound()
    {
        audioSource.PlayOneShot(death);
    }

    public void PlayStepsAudios()
    {
        audioSourceSteps.PlayOneShot(steps[SelectOneAudioRandom(steps)]);
    }

    public void PlayMaxAmmoAudio()
    {
        if (CanPlayOneAudio(50))
        {
            audioSource.PlayOneShot(maxAmmo);
        }
    }

    public void PlayKaboomAudio()
    {
        if (CanPlayOneAudio(50))
        {
            audioSource.PlayOneShot(kaboom);
        }
    }

    public void PlayDobulePointsAudio()
    {
        if (CanPlayOneAudio(50))
        {
            audioSource.PlayOneShot(doublePoints);
        }
    }

    public void PlayInstaKillAduio()
    {
        if (CanPlayOneAudio(50))
        {
            audioSource.PlayOneShot(instaKill);
        }
    }

    public void PlaySongsParts(int part)
    {
        if (CanPlayOneAudio(100))
        {
            switch (part)
            {
                case 1:
                    audioSource.PlayOneShot(songParts[0]);
                    break;
                case 2:
                    audioSource.PlayOneShot(songParts[1]);
                    break;
                case 3:
                    audioSource.PlayOneShot(songParts[2]);
                    break;
                case 4:
                    audioSource.PlayOneShot(songParts[3]);
                    break;
            }
        }
    }

    public void PlayPowerOnAudio()
    {
        if (CanPlayOneAudio(100))
        {
            audioSource.PlayOneShot(power);
        }
    }

    public void PlayNoPowerAudio()
    {
        if (CanPlayOneAudio(30))
        {
            audioSource.PlayOneShot(noPower[SelectOneAudioRandom(noPower)]);
        }
    }

    public void PlayNoMoneyAudio()
    {
        if (CanPlayOneAudio(80))
        {
            audioSource.PlayOneShot(noMoney[SelectOneAudioRandom(noMoney)]);
        }
    }

    public void PlayPlayerDamageAudio()
    {
        if (CanPlayOneAudio(50))
        {
            audioSource.PlayOneShot(playerDamage[SelectOneAudioRandom(playerDamage)]);
        }
    }

    public void PlayHeadShootAudio()
    {
        if (CanPlayOneAudio(30))
        {
            audioSource.PlayOneShot(headShoot[SelectOneAudioRandom(headShoot)]);
        }
    }

    public void PlayKillZombieAudio()
    {
        if (CanPlayOneAudio(20))
        {
            audioSource.PlayOneShot(killZombie[SelectOneAudioRandom(killZombie)]);
        }
    }

    public void PlayLowAmmoAudio()
    {
        if (CanPlayOneAudio(30))
        {
            audioSource.PlayOneShot(lowAmmo);
        }
    }

    public void PlayNoAmmoAudio()
    {
        if (CanPlayOneAudio(50))
        {
            audioSource.PlayOneShot(noAmmo[SelectOneAudioRandom(noAmmo)]);
        }
    }
}
