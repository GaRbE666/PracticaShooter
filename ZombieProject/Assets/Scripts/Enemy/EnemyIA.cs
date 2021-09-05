using UnityEngine.AI;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemyIA : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private EnemyHealth enemyHealth;
    [SerializeField] private LevelManager levelManager;
    public EnemyScriptable zScriptable;
    public int pointsReward;
    private EnemyAnimations enemyAnimations;
    private Transform player;
    public bool isAttacking;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyAnimations = GetComponent<EnemyAnimations>();
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
                agent.SetDestination(player.position);
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
        if (levelManager.easyLevel)
        {
            agent.speed = zScriptable.walkSpeed;
        }

        if (levelManager.hardLevel)
        {
            agent.speed = zScriptable.runSpeed;
        }
    }

    private void CheckDistanceToPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < zScriptable.distanceToAttack && !isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        agent.ResetPath();
        enemyAnimations.AttackAnim();
        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }
}
