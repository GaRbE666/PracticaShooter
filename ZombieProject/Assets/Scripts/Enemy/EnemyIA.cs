using UnityEngine.AI;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemyIA : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private EnemyHealth enemyHealth;
    public EnemyScriptable zScriptable;
    public int pointsReward;
    public bool isAttacking;

    private EnemyAnimations _enemyAnimations;
    private Transform _player;
    private LevelManager _levelManager;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _enemyAnimations = GetComponent<EnemyAnimations>();
        _levelManager = FindObjectOfType<LevelManager>();
    }

    private void Start()
    {
        pointsReward = zScriptable.pointReward;
        ChangeSpeedEnemy();
    }

    private void Update()
    {
        if (!enemyHealth.die)
        {
            if (!isAttacking)
            {
                agent.SetDestination(_player.position);
            }
            CheckDistanceToPlayer();
        }
        else
        {
            agent.speed = 0;
        } 
    }

    private void ChangeSpeedEnemy()
    {
        if (_levelManager.easyLevel)
        {
            agent.speed = zScriptable.walkSpeed;
        }

        if (_levelManager.hardLevel)
        {
            agent.speed = zScriptable.runSpeed;
        }
    }

    private void CheckDistanceToPlayer()
    {
        if (Vector3.Distance(transform.position, _player.position) < zScriptable.distanceToAttack && !isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        agent.ResetPath();
        _enemyAnimations.AttackAnim();
        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }
}
