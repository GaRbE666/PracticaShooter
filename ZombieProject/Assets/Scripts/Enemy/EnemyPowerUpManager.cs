using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPowerUpManager : MonoBehaviour
{
    private EnemyHealth _enemyHealth;
    private EnemyIA _enemyIA;
    private Item _itemIK;
    private Item _itemDP;

    private void Awake()
    {
        try
        {
            _itemDP = GameObject.Find("DoublePointsActived").GetComponent<Item>();
            _itemIK = GameObject.Find("InstakillActived").GetComponent<Item>();
        }
        catch (System.Exception)
        {
            Debug.LogWarning("No PowerUp actived");
        }
        _enemyHealth = GetComponent<EnemyHealth>();
        _enemyIA = GetComponent<EnemyIA>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfInstaKillIsActived();

        CheckIfDoublePointsIsActived();
    }

    private void CheckIfInstaKillIsActived()
    {
        if (_itemIK != null && _itemIK.instakillActived && !_enemyHealth.die)
        {
            _enemyHealth.currentHealth = 1;
        }
    }

    private void CheckIfDoublePointsIsActived()
    {
        if (_itemDP != null && _itemDP.doublePointsActived && !_enemyHealth.die)
        {
            if (_enemyIA.pointsReward / 2 == _enemyIA.zScriptable.pointReward && _enemyIA.pointsForHitReward / 2 == _enemyIA.zScriptable.pointsForHit)
            {
                return;
            }
            else
            {
                _enemyIA.pointsReward *= 2;
                _enemyIA.pointsForHitReward *= 2;
            }
        }
    }
}
