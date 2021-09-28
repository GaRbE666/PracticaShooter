using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    [SerializeField] private AudioClip mapSong;
    public int partActived;
    private PlayerAudio _playerAudio;
    private AudioSource _audioSource;
    private bool _songFinished;

    private void Awake()
    {
        _playerAudio = FindObjectOfType<PlayerAudio>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (partActived >= 3 && !_audioSource.isPlaying && !_songFinished)
        {
            StartCoroutine(PlayMapSong());
        }
    }

    public void PlayAudioPart()
    {
        _playerAudio.PlaySongsParts(partActived);
    }

    private IEnumerator PlayMapSong()
    {
        _songFinished = true;
        yield return new WaitForSeconds(1f);
        _audioSource.PlayOneShot(mapSong);
        partActived++;
        yield return new WaitForSeconds(5f);
        PlayAudioPart();
    }
}
