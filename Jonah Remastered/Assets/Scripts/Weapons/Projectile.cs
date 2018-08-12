using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public LayerMask collisionLayers;

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

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (collisionLayers == (collisionLayers | (1 << other.layer)))
        {
            if (other.GetComponent<IHealth>() != null)
                other.GetComponent<IHealth>().Damage(damage);

            Destroy(this.gameObject);
        }
    }
}
