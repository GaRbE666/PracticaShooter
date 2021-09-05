using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    private PlayerMovement _playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage()
    {
        currentHealth--;

        if (currentHealth <= 0)
        {
            Die();
        }

    }

    private void Die()
    {
        _playerMovement.enabled = false;
        Debug.Log("Estoy muerto");
    }
}
