using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float accelerationTime = 0.1f;

    private PlayerMotor motor;
    private Vector2 velocity;
    private Vector2 input;

    private float velocityXSmoothing;
    private float velocityYSmoothing;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        UpdateInputDirection();
        Move();
    }

    private void Move()
    {
        float targetVelocityX = input.x * speed;
        float targetVelocityY = input.y * speed;

        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, accelerationTime);
        velocity.y = Mathf.SmoothDamp(velocity.y, targetVelocityY, ref velocityYSmoothing, accelerationTime);

        motor.Move(velocity * Time.deltaTime);
    }

    private void UpdateInputDirection()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}
