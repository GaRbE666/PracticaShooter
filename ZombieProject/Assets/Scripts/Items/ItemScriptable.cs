using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemScriptable : ScriptableObject
{
    public string itemName;
    public float timeToDestroy;
    public enum itemEnumType { MaxAmmo, InstaKill, DoublePoints, Cash, Kaboom};
    public itemEnumType itemType;
}
