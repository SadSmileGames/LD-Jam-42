using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{
    public delegate void OnDeathDelegate();
    public static OnDeathDelegate OnDeath;
    public AudioClip deathSound;

    public int health = 100;

    public GameObject deathEffect;

    [HideInInspector]
    public int currentHealth;

    private AudioSource audioSource;

    void Start()
    {
        currentHealth = health;
        audioSource = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSource>();
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

        audioSource.PlayOneShot(deathSound);

        if (OnDeath != null)
            OnDeath();

        Destroy(this.gameObject);
    }
}
