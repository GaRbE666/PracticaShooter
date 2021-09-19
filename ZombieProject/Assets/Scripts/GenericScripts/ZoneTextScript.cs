using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoneTextScript : MonoBehaviour
{
    [SerializeField] private Text zoneText;
    [SerializeField] private string zoneName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            zoneText.text = zoneName;
            zoneText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            zoneText.text = zoneName;
            zoneText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            zoneText.gameObject.SetActive(false);
        }
    }
}
