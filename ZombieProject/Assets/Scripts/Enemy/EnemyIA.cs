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
    [SerializeField] private Transform spawnStartParticles;
    [SerializeField] private GameObject particlePrefab;
    [HideInInspector] public int pointsReward;
    [HideInInspector] public int pointsForHitReward;
    [HideInInspector] public bool isAttacking;
    [HideInInspector] public bool relaxZombies;
    [HideInInspector] public bool angryZombies;

    private EnemyAnimations _enemyAnimations;
    private PlayerMovement _player;
    private GameManager _gameManager;
    private bool _canMove;

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
        PlayStartParticleSpawn();
        StartCoroutine(EnemyCanMoveCoroutine());
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
                    if (_canMove)
                    {
                        MoveZombie();
                    }
                }
                CheckDistanceToPlayer();
            }
        }
        else
        {
            agent.speed = 0;
        } 
    }

    private void MoveZombie()
    {
        agent.SetDestination(_player.transform.position);
    }

    private IEnumerator EnemyCanMoveCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        _canMove = true;
    }

    private void PlayStartParticleSpawn()
    {
        GameObject particleClone = Instantiate(particlePrefab, spawnStartParticles.position, spawnStartParticles.rotation);
        particleClone.transform.SetParent(spawnStartParticles);
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
