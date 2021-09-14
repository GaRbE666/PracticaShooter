using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemManager : MonoBehaviour
{

    public List<GunShoot> gunShoots;
    public List<GrenadeThrower> grenades;

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
    
}
