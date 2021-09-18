using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public float timeToRecoverHP;
    [SerializeField] private Image damageImage;
    [SerializeField] private Sprite[] damageSprite;

    private PlayerMovement _playerMovement;
    public bool _isDie;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
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
        
        if (currentHealth <= 0)
        {
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

    private void Die()
    {
        _isDie = true;
        damageImage.sprite = damageSprite[1];
        _playerMovement.enabled = false;
        currentHealth = 0;
    }
}
