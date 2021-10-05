using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyScriptable : ScriptableObject
{
    public string zombieName;
    [Header("Health Settings")]
    public int startedHealth;
    public int maxHealth;
    [Header("Movement Settings")]
    public float walkSpeed;
    public float runSpeed;
    [Header("Attack Settings")]
    public float distanceToAttack;
    public float distanceToStop;
    [Header("Points Settings")]
    public int pointReward;
    public int pointsForHit;
    [Header("Items Settings")]
    public int dropPercent;
    public List<GameObject> items;
}
