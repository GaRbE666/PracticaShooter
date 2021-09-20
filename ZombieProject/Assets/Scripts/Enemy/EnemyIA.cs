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
    private GameManager _gameManager;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _enemyAnimations = GetComponent<EnemyAnimations>();
        _gameManager = FindObjectOfType<GameManager>();
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
        if (_gameManager.currentRound <= 3)
        {
            agent.speed = zScriptable.walkSpeed;
        }

        if (_gameManager.currentRound > 3)
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
        if (CheckDistanceToStop())
        {
            agent.ResetPath();
        }
        _enemyAnimations.AttackAnim();
        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }

    private bool CheckDistanceToStop()
    {
        return Vector3.Distance(transform.position, _player.position) < zScriptable.distanceToStop;
    }
}
