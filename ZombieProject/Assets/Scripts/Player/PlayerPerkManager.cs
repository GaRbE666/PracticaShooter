using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPerkManager : MonoBehaviour
{
    [SerializeField] private Image[] perksImages;
    public bool speedColaActive;
    public int perksAdquired;
    private PlayerHealth playerHealth;
    private GunShoot gunsShoots;
    private PlayerMovement playerMovement;
    public bool quickAdquired;
    public bool juggerAdquired;
    public bool speedColaAdquired;
    public bool doubleTapAdquired;
    public bool stamminUpAdquired;

    private void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        if (playerHealth._isDie)
        {
            RemoveAllPerks();
        }
    }

    public void SelectPerk(ScriptablePerk.TypeOfPerks perkType, Sprite perkIcon)
    {
        
        switch (perkType)
        {
            case ScriptablePerk.TypeOfPerks.QuickRevive:
                perksAdquired++;
                quickAdquired = true;
                QuickReviveEffect();
                break;
            case ScriptablePerk.TypeOfPerks.Juggernaut:
                perksAdquired++;
                juggerAdquired = true;
                JuggernautEffect();
                break;
            case ScriptablePerk.TypeOfPerks.SpeedCola:
                speedColaActive = true;
                perksAdquired++;
                speedColaAdquired = true;
                SpeedColaEffect();
                break;
            case ScriptablePerk.TypeOfPerks.DoubleTap:
                DoubleTapEffect();
                doubleTapAdquired = true;
                perksAdquired++;
                break;
            case ScriptablePerk.TypeOfPerks.StamminUp:
                StammingUpEffect();
                stamminUpAdquired = true;
                perksAdquired++;
                break;
        }
        SetUpPerkImage(perkIcon);
    }

    private void SetUpPerkImage(Sprite perkIcon)
    {
        switch (perksAdquired)
        {
            case 1:
                perksImages[0].sprite = perkIcon;
                perksImages[0].enabled = true;
                break;
            case 2:
                perksImages[1].sprite = perkIcon;
                perksImages[1].enabled = true;
                break;
            case 3:
                perksImages[2].sprite = perkIcon;
                perksImages[2].enabled = true;
                break;
            case 4:
                perksImages[3].sprite = perkIcon;
                perksImages[3].enabled = true;
                break;
            case 5:
                perksImages[4].sprite = perkIcon;
                perksImages[4].enabled = true;
                break;
            default:
                break;
        }
    }

    private void QuickReviveEffect()
    {
        playerHealth.timeToRecoverHP = 4;
    }

    //private void RestoreQuickReviveEffect()
    //{
    //    playerHealth.timeToRecoverHP = 6;
    //}

    private void SpeedColaEffect()
    {
        PlayerGuns playerGuns = GetComponent<PlayerGuns>();
        foreach (GunShoot gun in playerGuns.gunShoots)
        {
            gun.reloadTime /= 2;
        }
    }

    //private void RestoreSpeedColaEffect()
    //{
    //    gunsShoots = FindObjectOfType<GunShoot>();
    //    gunsShoots.reloadTime *= 2;
    //}

    private void JuggernautEffect()
    {
        playerHealth.maxHealth = 5;
        playerHealth.currentHealth = playerHealth.maxHealth;
    }

    //private void RestoreJuggernautEffect()
    //{
    //    playerHealth.maxHealth = 3;
    //    playerHealth.currentHealth = playerHealth.maxHealth;
    //}

    private void DoubleTapEffect()
    {
        PlayerGuns playerGuns = GetComponent<PlayerGuns>();
        foreach (GunShoot gun in playerGuns.gunShoots)
        {
            if (!gun.gunScriptable.singleShoot)
            {
                gun.fireRate *= 1.5f;
            }
        }

    }

    private void StammingUpEffect()
    {
        playerMovement.playerRunSpeed = 12;
    }

    private void RemoveAllPerks()
    {
        perksAdquired = 0;
        for (int i = 0; i < perksImages.Length; i++)
        {
            perksImages[i].enabled = false;
        }
        speedColaActive = false;
        //RestoreQuickReviveEffect();
        //RestoreSpeedColaEffect();
        //RestoreJuggernautEffect();
    }
}
