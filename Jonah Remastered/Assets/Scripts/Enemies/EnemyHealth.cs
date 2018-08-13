using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{
    public int health = 100;

    public GameObject deathEffect;

    [HideInInspector]
    public int currentHealth;

    void Start()
    {
        currentHealth = health;
    }

    public void Damage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
