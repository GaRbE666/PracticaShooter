using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerOn : MonoBehaviour
{
    [SerializeField] private Text powerText;
    [SerializeField] private Animation _animation;
    public delegate void Power();
    public event Power PowerOnReleased;
    

    private void Start()
    {
        powerText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            powerText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            powerText.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                PowerOnReleased?.Invoke();
                _animation.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            powerText.gameObject.SetActive(false);
        }
    }
}
