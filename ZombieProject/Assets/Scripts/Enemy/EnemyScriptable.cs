using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyScriptable : ScriptableObject
{
    public string zombieName;
    public int maxHealth;
    public float walkSpeed;
    public float runSpeed;
    public float distanceToAttack;
    public float distanceToStop;
    public int pointReward;
    public int pointsForHit;
    public List<GameObject> items;
}
