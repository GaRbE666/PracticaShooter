using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimations : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private GunShoot gunShoot;

    private PlayerPerkManager playerPerkManager;
    private PlayerMovement playerMovement;
    private int running = Animator.StringToHash("isRunning");
    private int shooting = Animator.StringToHash("shoot");
    private int reloading = Animator.StringToHash("reload");
    private int reloadingMulti = Animator.StringToHash("reloadMultiplier");
    private int drawGun = Animator.StringToHash("draw");

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerPerkManager = FindObjectOfType<PlayerPerkManager>();
    }

    private void Update()
    {
        GunRunningAnim();
        ShootAnim();
        ReloadAnim();
        AddMultiplierSpeed();
    }

    private void ReloadAnim()
    {
        if (gunShoot.isRealoading)
        {
            animator.SetBool(reloading, true);
        }
        else
        {
            animator.SetBool(reloading, false);
        }
    }

    private void ShootAnim()
    {
        if (gunShoot.isShooting)
        {
            animator.SetTrigger(shooting);
        }
    }

    private void GunRunningAnim()
    {
        if (playerMovement.isRunning && playerMovement._isGrounded)
        {
            animator.SetBool(running, true);
        }
        else
        {
            animator.SetBool(running, false);
        }
    }

    private void AddMultiplierSpeed()
    {
        if (playerPerkManager.speedColaActive)
        {
            animator.SetFloat(reloadingMulti, 2.0f);
        }
        else
        {
            animator.SetFloat(reloadingMulti, 1.0f);
        }
    }

    public void DrawGunAgain()
    {
        animator.SetTrigger(drawGun);
    }
}
