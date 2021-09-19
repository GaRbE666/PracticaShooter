using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GunScriptable : ScriptableObject
{
    public string gunName;
    public float price;
    public int maxBulletsInBedroom;
    public int maxBulletPerCharger;
    public float reloadingtime;
    public float fireRate;
    public int damage;
    public float range;
    public bool singleShoot;
    public enum AmmoType { pistol, shotgun, rifle, sniper, grenade}
    public AmmoType ammoType;
}
