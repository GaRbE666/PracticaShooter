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
                QuickReviveEffect();
                break;
            case ScriptablePerk.TypeOfPerks.Juggernaut:
                perksAdquired++;
                JuggernautEffect();
                break;
            case ScriptablePerk.TypeOfPerks.SpeedCola:
                speedColaActive = true;
                perksAdquired++;
                SpeedColaEffect();
                break;
            case ScriptablePerk.TypeOfPerks.DoubleTap:
                DoubleTapEffect();
                perksAdquired++;
                break;
            case ScriptablePerk.TypeOfPerks.StamminUp:
                StammingUpEffect();
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

    private void RestoreQuickReviveEffect()
    {
        playerHealth.timeToRecoverHP = 6;
    }

    private void SpeedColaEffect()
    {
        gunsShoots = FindObjectOfType<GunShoot>();
        gunsShoots.reloadTime /= 2;
    }

    private void RestoreSpeedColaEffect()
    {
        gunsShoots = FindObjectOfType<GunShoot>();
        gunsShoots.reloadTime *= 2;
    }

    private void JuggernautEffect()
    {
        playerHealth.maxHealth = 5;
        playerHealth.currentHealth = playerHealth.maxHealth;
    }

    private void RestoreJuggernautEffect()
    {
        playerHealth.maxHealth = 3;
        playerHealth.currentHealth = playerHealth.maxHealth;
    }

    private void DoubleTapEffect()
    {
        gunsShoots = FindObjectOfType<GunShoot>();
        if (!gunsShoots.gunScriptable.singleShoot)
        {
            gunsShoots.fireRate *= 1.5f;
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
        RestoreQuickReviveEffect();
        RestoreSpeedColaEffect();
        RestoreJuggernautEffect();
    }
}
