using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScriptablePerk : ScriptableObject
{
    public string name;
    public string description;
    public int cost;
    public Sprite perkIcon;
    public enum TypeOfPerks { QuickRevive, Juggernaut, SpeedCola, DoubleTap, StamminUp};
    public TypeOfPerks perkType;
}
