using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerOn : MonoBehaviour
{
    [SerializeField] private Text powerText;
    [SerializeField] private Animation _animation;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject lights;
    [SerializeField] private GameObject mainLights;
    [SerializeField] private GameObject[] electricitySparks;
    public delegate void Power();
    public event Power PowerOnReleased;

    private GameManager _gameManager;
    private PlayerAudio _playerAudio;
    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _playerAudio = FindObjectOfType<PlayerAudio>();
    }

    private void Start()
    {
        powerText.gameObject.SetActive(false);
        SetPowerText("Mantén F para encender");
        DisableAllLights();
    }

    private void DisableAllLights()
    {
        for (int i = 0; i < lights.transform.childCount; i++)
        {
            lights.transform.GetChild(i).gameObject.SetActive(false);
        }
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!_gameManager.powerOn)
            {
                ActiveText();
                PlayerPressKey();
            }
            else
            {
                DesactiveText();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DesactiveText();
        }
    }

    private void PlayerPressKey()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PowerOnReleased?.Invoke();
            ModifyLights();
            ReproduceAudios();
            PlaySparks();
        }
    }

    private void ModifyLights()
    {
        TurnOnLights();
        UpgradeIntensityMainLight();
    }

    private void PlaySparks()
    {
        for (int i = 0; i < electricitySparks.Length; i++)
        {
            electricitySparks[i].GetComponent<ParticleSystem>().Play();
        }
    }

    private void TurnOnLights()
    {
        for (int i = 0; i < lights.transform.childCount; i++)
        {
            lights.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    private void UpgradeIntensityMainLight()
    {
        for (int i = 0; i < mainLights.transform.childCount; i++)
        {
            mainLights.transform.GetChild(i).GetComponent<Light>().intensity = .3f;
        }
    }

    private void ReproduceAudios()
    {
        _animation.Play();
        audioSource.Play();
        _playerAudio.PlayPowerOnAudio();
    }

    private void SetPowerText(string text)
    {
        powerText.text = text;
    }

    private void ActiveText()
    {
        powerText.gameObject.SetActive(true);
    }

    private void DesactiveText()
    {
        powerText.gameObject.SetActive(false);
    }
}
