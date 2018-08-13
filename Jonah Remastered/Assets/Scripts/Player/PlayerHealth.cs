using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    public delegate void OnPlayerDamageDelegate();

    public static OnPlayerDamageDelegate OnPlayerDeath;
    public static OnPlayerDamageDelegate OnPlayerDamage;

    public int health = 5;
    
    private int currentHealth;

    void Start ()
    {
        currentHealth = health;
	}

    public void Damage(int amount)
    {
        currentHealth--;

        if (OnPlayerDamage != null)
            OnPlayerDamage();

        if(currentHealth <= 0)
        {
            if (OnPlayerDeath != null)
                OnPlayerDeath();

            Die();
        }
    }

    private void Die()
    {
        this.gameObject.SetActive(false);
    }
}
