using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collision2D))]
public class PlayerMotor : MonoBehaviour
{
    private Collision2D collision;

    private void Start()
    {
        collision = GetComponent<Collision2D>();
    }

    public void Move(Vector2 velocity)
    {
        collision.CheckHorizontalCollisions(ref velocity);
        collision.CheckVerticalCollisions(ref velocity);

        transform.Translate(velocity);
    }

    public void Dash(Vector3 targetPos)
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, 1f);
    }
}
