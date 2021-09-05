using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyHealth enemyHealth;
    [SerializeField] private EnemyIA enemyIA;
    [SerializeField] private LevelManager levelManager;

    private int die = Animator.StringToHash("die");
    private int attack = Animator.StringToHash("attack");
    private int run = Animator.StringToHash("isRunning");
    private int walk = Animator.StringToHash("isWalking");

    private void Update()
    {
        DieAnim();

        RunAnim();
        WalkAnim();
    }

    private void RunAnim()
    {
        if (levelManager.hardLevel)
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
        if (levelManager.easyLevel)
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
