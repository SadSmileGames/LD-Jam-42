using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    public int health = 100;

    [HideInInspector]
    public int currentHealth;

    void Start ()
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
        Debug.Log("U r already daed!");
    }
}
