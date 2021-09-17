using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimations : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private GunShoot gunShoot;

    private PlayerMovement playerMovement;
    private int running = Animator.StringToHash("isRunning");
    private int shooting = Animator.StringToHash("shoot");
    public int reloading = Animator.StringToHash("reload");

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        GunRunningAnim();
        ShootAnim();
        ReloadAnim();
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
        if (playerMovement.isRunning)
        {
            animator.SetBool(running, true);
        }
        else
        {
            animator.SetBool(running, false);
        }
    }
}
