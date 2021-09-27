using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
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
}
