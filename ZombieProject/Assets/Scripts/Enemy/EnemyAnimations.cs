using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyHealth enemyHealth;
    [SerializeField] private EnemyIA enemyIA;
    
    private GameManager _gameManager;
    private int die = Animator.StringToHash("die");
    private int attack = Animator.StringToHash("attack");
    private int run = Animator.StringToHash("isRunning");
    private int walk = Animator.StringToHash("isWalking");

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        DieAnim();

        RunAnim();
        WalkAnim();
    }

    private void RunAnim()
    {
        if (_gameManager.currentRound > 3)
        {
            animator.SetBool(run, true);
        }
        else
        {
            animator.SetBool(run, false);
        }
    }

    private void WalkAnim()
    {
        if (_gameManager.currentRound <= 3)
        {
            animator.SetBool(walk, true);
        }
        else
        {
            animator.SetBool(walk, false);
        }
    }

    public void AttackAnim()
    {
        animator.SetTrigger(attack);
    } 

    private void DieAnim()
    {
        if (enemyHealth.die)
        {
            //animator.SetBool(die, true);
            animator.Play("Z_FallingBack", 0);
        }
        //else
        //{
        //    animator.SetBool(die, false);
        //} 
    }
}
