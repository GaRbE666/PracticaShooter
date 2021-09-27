using UnityEngine.AI;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemyIA : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private EnemyHealth enemyHealth;
    [SerializeField] private Transform blindTarget;
    public EnemyScriptable zScriptable;
    public int pointsReward;
    public int pointsForHitReward;
    public bool isAttacking;
    public bool relaxZombies;
    public bool angryZombies;

    private EnemyAnimations _enemyAnimations;
    private PlayerMovement _player;
    private GameManager _gameManager;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerMovement>();
        _enemyAnimations = GetComponent<EnemyAnimations>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        pointsReward = zScriptable.pointReward;
        pointsForHitReward = zScriptable.pointsForHit;
        ChangeSpeedEnemy();
    }

    private void Update()
    {
        if (!enemyHealth.die)
        {
            if (_gameManager.playerTeleported)
            {
                PlayerIsInPAPRoom();
            }
            else
            {
                if (!isAttacking)
                {
                    agent.SetDestination(_player.transform.position);
                }
                CheckDistanceToPlayer();
            }
        }
        else
        {
            agent.speed = 0;
        } 
    }

    private void PlayerIsInPAPRoom()
    {
        agent.ResetPath();
        agent.SetDestination(blindTarget.position);
    }

    private void ChangeSpeedEnemy()
    {
        if (_gameManager.currentRound <= 3)
        {
            relaxZombies = true;
            angryZombies = false;
            agent.speed = zScriptable.walkSpeed;
        }

        if (_gameManager.currentRound > 3)
        {
            relaxZombies = false;
            angryZombies = true;
            agent.speed = zScriptable.runSpeed;
        }
    }

    private void CheckDistanceToPlayer()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) < zScriptable.distanceToAttack && !isAttacking)
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
        return Vector3.Distance(transform.position, _player.transform.position) < zScriptable.distanceToStop;
    }
}
