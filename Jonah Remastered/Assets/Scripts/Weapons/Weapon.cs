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
    private int bulletsInClip;
    private bool isReloading;

    private AudioSource source;

    private void Start()
    {
        firePoint = this.transform.GetChild(1).transform;
        timeBetweenShots = 1 / weaponStats.bulletsPerSeconds;
        bulletsInClip = weaponStats.clipSize;

        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(weaponStats.mode == WeaponData.FireMode.Semi)
        {
            if (Input.GetButtonDown("Fire1"))
                Shoot();
        }
        else if (weaponStats.mode == WeaponData.FireMode.Automatic)
        {
            if (Input.GetButton("Fire1"))
                Shoot();
        }
    }

    private void Shoot()
    {
        if (!(Time.time > nextShotTime) || isReloading)
            return;

        if (bulletsInClip <= 0 && !isReloading)
            StartCoroutine(Reload());
            

        nextShotTime = Time.time + timeBetweenShots;
        Projectile clone = Instantiate(weaponStats.projectile, firePoint.position, firePoint.rotation) as Projectile;
        clone.SetSpeed(weaponStats.muzzleVelocity);
        clone.SetDamage(weaponStats.damage);

        Shooting.Invoke();
        PlayShootSound();
        bulletsInClip--;
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(weaponStats.reloadTime);
        bulletsInClip = weaponStats.clipSize;
        isReloading = false;
    }

    private void PlayShootSound()
    {
        source.pitch = Random.Range(0.75f, 1f);
        source.PlayOneShot(weaponStats.shootSound);
    }
}
