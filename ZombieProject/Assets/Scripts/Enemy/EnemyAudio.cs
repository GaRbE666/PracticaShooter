using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] agressiveSounds;
    [SerializeField] private AudioClip[] normalSounds;
    [SerializeField] private AudioClip headShoot;
    private bool canSound;
    private EnemyHealth _enemyHealth;
    private EnemyIA _enemyAI;

    private void Awake()
    {
        _enemyHealth = FindObjectOfType<EnemyHealth>();
        _enemyAI = FindObjectOfType<EnemyIA>();
    }

    private void Start()
    {
        canSound = true;

    }

    private void Update()
    {
        if (canSound && !_enemyHealth.die)
        {
            StartCoroutine(ShotOneAudio());
        }
    }

    public void PlayHeadShoot()
    {
        audioSource.PlayOneShot(headShoot);
    }

    private IEnumerator ShotOneAudio()
    {
        canSound = false;
        PlayZombieScream();
        yield return new WaitForSeconds(RandomBetweenZombieScream());
        canSound = true;
    }

    private void PlayZombieScream()
    {
        audioSource.PlayOneShot(SelectOneAudio());
    }

    private AudioClip SelectOneAudio()
    {
        int numRand;
        if (_enemyAI.relaxZombies && !_enemyAI.angryZombies)
        {
            numRand = Random.Range(0, normalSounds.Length);
            return normalSounds[numRand];
        }else
        {
            numRand = Random.Range(0, agressiveSounds.Length);
            return agressiveSounds[numRand];
        }

    }

    private int RandomBetweenZombieScream()
    {
        int numRan = Random.Range(2, 4);
        return numRan;
    }
}
