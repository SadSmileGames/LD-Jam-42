using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public AudioClip dashSound;

    public float speed = 5f;
    public float moveAccelerationTime = 0.1f;

    public float dashDistance = 5f;
    public float dashTime = 0.2f;
    public float dashCoolDown = 1f;

    public UnityEngine.Events.UnityEvent Dashing;

    [HideInInspector]
    public bool isDashing = false;
    private float dashDuration;
    private float dashCoolDownTimer;

    private PlayerMotor motor;
    private Vector2 velocity;
    private Vector2 input;

    private float velocityXSmoothing;
    private float velocityYSmoothing;

    private AudioSource audioSource;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
        audioSource = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!isDashing)
        {
            dashCoolDownTimer -= Time.deltaTime;
            UpdateInputDirection();
            Move();

            if (Input.GetButtonDown("Jump") && dashCoolDownTimer <= 0)
                Dash();
        }
        else
        {
            motor.Move(velocity * Time.deltaTime);
            dashDuration -= Time.deltaTime;

            if(dashDuration <= 0)
            {
                dashDuration = 0;
                isDashing = false;
            }
        } 
    }

    private void Move()
    {
        float targetVelocityX = input.x * speed;
        float targetVelocityY = input.y * speed;

        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, moveAccelerationTime);
        velocity.y = Mathf.SmoothDamp(velocity.y, targetVelocityY, ref velocityYSmoothing, moveAccelerationTime);
        motor.Move(velocity * Time.deltaTime);
    }

    private void Dash()
    {
        Vector2 dashDirection = input;
        float dashVelocity = dashDistance / dashTime;
        isDashing = true;
        Dashing.Invoke();

        velocity = input * dashVelocity;
        dashDuration = dashTime;
        dashCoolDownTimer = dashCoolDown;

        audioSource.PlayOneShot(dashSound);
    }

    private void UpdateInputDirection()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    public bool IsMoving()
    {
        if (Mathf.Abs(velocity.x) > 0.2f || Mathf.Abs(velocity.y) > 0.2f)
            return true;
        else
            return false;
    }
}
