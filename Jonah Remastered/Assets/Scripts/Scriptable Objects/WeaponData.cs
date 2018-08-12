using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Data", menuName = "Weapons/Weapon", order = 2)]
public class WeaponData : ScriptableObject
{
    public enum FireMode { Semi, Automatic }

    [Header("General")]
    public Projectile projectile;

    [Header("Firerate")]
    public FireMode mode;
    public float bulletsPerSeconds = 1f;

    [Header("Projectile Options")]
    public int damage = 10;
    public float muzzleVelocity = 15f;

    [Header("Reloading")]
    public float reloadTime = 0.5f;
    public int clipSize = 10;
}
