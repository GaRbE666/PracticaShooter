using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public float timeToRecoverHP;
    [SerializeField] private Image crossHire;
    [SerializeField] private Image damageImage;
    [SerializeField] private Sprite[] damageSprite;
    [SerializeField] private Animation deathAnimation;
    public delegate void PlayerDie();
    public event PlayerDie PlayerDieRelease;

    private PlayerMovement _playerMovement;
    private PlayerAudio _playerAudio;
    [HideInInspector] public bool _isDie;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerAudio = GetComponentInChildren<PlayerAudio>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        damageImage.enabled = false;
    }

    private void Update()
    {
        ChangeImageDamage();
    }

    public void TakeDamage()
    {
        currentHealth--;
        _playerAudio.PlayPlayerDamageAudio();
        if (currentHealth <= 0)
        {
            PlayerDieRelease?.Invoke();
            Die();
        }
        StartCoroutine(RecoverHP());
    }

    private void ChangeImageDamage()
    {
        if (!_isDie)
        {
            switch (currentHealth)
            {
                case 0:
                    damageImage.enabled = true;
                    damageImage.sprite = damageSprite[0];
                    break;
                case 1:
                    damageImage.enabled = true;
                    damageImage.sprite = damageSprite[1];
                    break;
                case 2:
                    damageImage.enabled = true;
                    damageImage.sprite = damageSprite[0];
                    break;
                default:
                    damageImage.enabled = false;
                    break;
            }
        }

    }

    private IEnumerator RecoverHP()
    {
        yield return new WaitForSeconds(timeToRecoverHP);
        if (!_isDie)
        {
            currentHealth++;
        }
    }

    private void DisableAllComponents()
    {
        _playerMovement.enabled = false;
        GetComponent<CharacterController>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponentInChildren<WeaponSwitching>().enabled = false;
        DisableGuns();
        enabled = false;
    }

    private void DisableGuns()
    {
        GunShoot[] guns = GetComponentsInChildren<GunShoot>();
        foreach (GunShoot gun in guns)
        {
            gun.gameObject.SetActive(false);
        }
    }

    private void Die()
    {
        _isDie = true;
        damageImage.sprite = damageSprite[1];
        damageImage.enabled = false;
        crossHire.enabled = false;
        currentHealth = 0;
        deathAnimation.Play();
        _playerAudio.PlayDeathSound();
        DisableAllComponents();
    }
}
