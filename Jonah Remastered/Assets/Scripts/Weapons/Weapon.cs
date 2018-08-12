using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum FireMode { Semi, Automatic }

    public Projectile projectile;

    public FireMode mode;
    public float bulletsPerSeconds = 1f;

    public int damage = 10;
    public float muzzleVelocity = 15f;

    public float reloadTime = 0.5f;
    public int clipSize = 10;

    public UnityEngine.Events.UnityEvent Shooting;

    private Transform firePoint;
    private float timeBetweenShots;
    private float nextShotTime;
    private int bulletsInClip;
    private bool isReloading;

    private void Start()
    {
        firePoint = this.transform.GetChild(1).transform;
        timeBetweenShots = 1 / bulletsPerSeconds;
        bulletsInClip = clipSize;
    }

    private void Update()
    {
        if(mode == FireMode.Semi)
        {
            if (Input.GetButtonDown("Fire1"))
                Shoot();
        }
        else if (mode == FireMode.Automatic)
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
        Projectile clone = Instantiate(projectile, firePoint.position, firePoint.rotation) as Projectile;
        clone.SetSpeed(muzzleVelocity);
        clone.SetDamage(damage);

        Shooting.Invoke();
        bulletsInClip--;
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        bulletsInClip = clipSize;
        isReloading = false;
    }
}
