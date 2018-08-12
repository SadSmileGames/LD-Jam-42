using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed = 10f;
    private int damage = 10;

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetDamage(int dmg)
    {
        this.damage = dmg;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed);
    }
}
