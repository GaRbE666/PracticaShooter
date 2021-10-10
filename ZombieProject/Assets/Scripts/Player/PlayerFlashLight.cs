using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlashLight : MonoBehaviour
{
    [SerializeField] private GameObject flashLight;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip flashLightSound;

    private bool flashLightOn;

    // Start is called before the first frame update
    void Start()
    {
        flashLight.SetActive(true);
        flashLightOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (flashLightOn)
            {
                TurnOffFlashLight();
            }
            else
            {
                TurnOnFlashLight();
            }
        }
    }

    private void TurnOnFlashLight()
    {
        PlayFlashLightSound();
        flashLightOn = true;
        flashLight.SetActive(true);
    }

    private void TurnOffFlashLight()
    {
        PlayFlashLightSound();
        flashLightOn = false;
        flashLight.SetActive(false);
    }

    private void PlayFlashLightSound()
    {
        audioSource.PlayOneShot(flashLightSound);
    }
}
