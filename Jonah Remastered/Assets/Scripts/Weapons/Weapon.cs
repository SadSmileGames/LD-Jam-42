using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData weaponStats;

    [Header("Event Handlers")]
    public UnityEngine.Events.UnityEvent Shooting;

    private Transform firePoint;
    private float timeBetweenShots;
    private float nextShotTime;

    private AudioSource source;

    private void Start()
    {
        firePoint = this.transform.GetChild(1).transform;
        timeBetweenShots = 1 / weaponStats.bulletsPerSeconds;

        source = GetComponent<AudioSource>();
    }

    public void SetFirepoint(Transform firePoint)
    {
        this.firePoint = firePoint;
    }

    public void SetFirePointDirection (Vector2 direction)
    {
        firePoint.right = direction;
    }

    public void Shoot()
    {
        if (!(Time.time > nextShotTime))
            return;

        nextShotTime = Time.time + timeBetweenShots;
        Projectile clone = Instantiate(weaponStats.projectile, firePoint.position, firePoint.rotation) as Projectile;
        clone.SetSpeed(weaponStats.muzzleVelocity);
        clone.SetDamage(weaponStats.damage);

        Shooting.Invoke();
        PlayShootSound();
    }

    private void PlayShootSound()
    {
        source.pitch = Random.Range(0.75f, 1f);
        source.PlayOneShot(weaponStats.shootSound);
    }
}
