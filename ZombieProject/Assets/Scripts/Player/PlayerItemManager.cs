using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItemManager : MonoBehaviour
{

    public List<GunShoot> gunShoots;
    public List<GrenadeThrower> grenades;

    private Text itemNameText;

    private void Awake()
    {
        itemNameText = FindObjectOfType<PowerUpName>().GetComponent<Text>();
    }

    public void MaxAmmo()
    {
        foreach (GunShoot gun in gunShoots)
        {
            gun.currentChargerAmmo = gun.gunScriptable.maxBulletPerCharger;
            gun.currentBedroomAmmo = gun.gunScriptable.maxBulletsInBedroom;
            if (gun.gameObject.activeSelf)
            {
                gun.UpdateAmmoTexts();
            }
            
        }

        foreach (GrenadeThrower grenade in grenades)
        {
            grenade._currentChargerAmmo = grenade.grenadeScriptable.maxBulletPerCharger;
            grenade._currentBedroomAmmo = grenade.grenadeScriptable.maxBulletsInBedroom;
            if (grenade.gameObject.activeSelf)
            {
                grenade.UpdateAmmoText();
            }
        }
    }

    public void ShowText(string powerUpName)
    {
        StartCoroutine(ShowTextCoroutine(powerUpName));
    }

    public IEnumerator ShowTextCoroutine(string powerUpName)
    {
        itemNameText.text = powerUpName;
        itemNameText.enabled = true;
        yield return new WaitForSeconds(3f);
        itemNameText.enabled = false;
    }


}
