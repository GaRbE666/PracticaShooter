using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public int pointsRequired;
    [SerializeField] private Text doorText;
    [SerializeField] private GameObject[] doors;
    [SerializeField] private bool electricityRequired;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorText.enabled = true;
            ChangeTextValue();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (!electricityRequired)
                {
                    PlayerScore playerScore = other.GetComponent<PlayerScore>();
                    if (playerScore.score >= pointsRequired)
                    {
                        doorText.enabled = false;
                        playerScore.QuitScore(pointsRequired);
                        DisableAllDoors();
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorText.enabled = false;
        }
    }

    private void ChangeTextValue()
    {
        if (electricityRequired)
        {
            doorText.text = "Electricity is required to open the door";
        }
        else
        {
            doorText.text = "Press F to open door (" + pointsRequired + ")";
        }
        
    }

    private void DisableAllDoors()
    {
        foreach (GameObject door in doors)
        {
            door.SetActive(false);
        }
    }
}
