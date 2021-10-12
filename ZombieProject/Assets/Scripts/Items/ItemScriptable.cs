using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class ItemScriptable : ScriptableObject
{
    public string itemName;
    public float timeToDestroy;
    public Sprite itemIcon;
    public bool useTimerText;
    public Text timerText;
    public enum itemEnumType { MaxAmmo, InstaKill, DoublePoints, Cash, Kaboom};
    public itemEnumType itemType;
}
