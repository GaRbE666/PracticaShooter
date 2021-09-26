using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PackAPunch : MonoBehaviour
{
    [SerializeField] private Text papText;
    [SerializeField] private int cost;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!other.GetComponentInChildren<GunShoot>().papActived)
            {
                SetPapText("Mantén F para comprar mejora de potenciadora (coste: " + cost + ")");
                ShowText();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GunShoot gunShootTemp = other.GetComponentInChildren<GunShoot>();
            if (!gunShootTemp.papActived)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    PlayerScore playerScoreTemp = other.GetComponent<PlayerScore>();
                    if (playerScoreTemp.score >= cost)
                    {
                        gunShootTemp.GetComponent<GunAnimations>().DrawGunAgain();
                        playerScoreTemp.QuitScore(cost);
                        gunShootTemp.papActived = true;
                        gunShootTemp.gunModel.GetComponent<SkinnedMeshRenderer>().material = gunShootTemp.papMaterial;
                        gunShootTemp.gunDamage *= 3;
                        DisableText();
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DisableText();
        }
    }

    private void SetPapText(string text)
    {
        papText.text = text;
    }

    private void ShowText()
    {
        papText.enabled = true;
    }

    private void DisableText()
    {
        papText.enabled = false;
    }
}
