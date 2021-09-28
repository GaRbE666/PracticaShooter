using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PackAPunch : MonoBehaviour
{
    [SerializeField] private Text papText;
    [SerializeField] private int cost;

    private PlayerAudio _playerAudio;

    private void Awake()
    {
        _playerAudio = FindObjectOfType<PlayerAudio>();
    }

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
                PlayerPressKey(gunShootTemp, other);
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

    private void UpgradeGun(GunShoot gunShootTemp, Collider other, PlayerScore playerScoreTemp)
    {
        gunShootTemp.GetComponent<GunAnimations>().DrawGunAgain();
        playerScoreTemp.QuitScore(cost);
        gunShootTemp.papActived = true;
        gunShootTemp.gunModel.GetComponent<SkinnedMeshRenderer>().material = gunShootTemp.papMaterial;
        gunShootTemp.gunDamage *= 3;
        DisableText();
    }

    private void PlayerPressKey(GunShoot gunShootTemp, Collider other)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayerScore playerScoreTemp = other.GetComponent<PlayerScore>();
            if (playerScoreTemp.score >= cost)
            {
                UpgradeGun(gunShootTemp, other, playerScoreTemp);
            }
            else
            {
                _playerAudio.PlayNoMoneyAudio();
            }
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
