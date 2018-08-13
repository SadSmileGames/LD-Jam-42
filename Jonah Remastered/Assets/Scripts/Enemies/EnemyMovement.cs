using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public delegate void OnTargetReachedDelegate(Transform actor);
    public static OnTargetReachedDelegate OnTargetReachedEvent;

    public float speed;
    public float stoppingDistance = 3f;

    private float distance;
    private Vector2 moveDirection;
    private Collision2D coll;

    private Vector2 velocity;

    private float velocityXSmoothing;
    private float velocityYSmoothing;


    private void Start()
    {
        coll = GetComponent<Collision2D>();
    }

    public void Move(Transform target)
    {
        if (target == null)
            return;

        CalculateDistance(transform.position, target.position);
        UpdateMoveDirection(target);
        velocity = moveDirection * speed * Time.deltaTime;

        if (distance > stoppingDistance)
        {
            coll.CheckHorizontalCollisions(ref velocity);
            coll.CheckVerticalCollisions(ref velocity);

            transform.Translate(velocity);
        }
        else
        {
            if (OnTargetReachedEvent != null)
                OnTargetReachedEvent(this.transform);
        } 
    }

    private void CalculateDistance(Vector2 from, Vector2 to)
    {
        distance = Vector2.Distance(from, to);
    }

    private Vector2 CalculateDirection (Vector2 from, Vector2 to)
    {
        moveDirection = Vector2.zero;

        Vector2 dir = to - from;
        return dir.normalized;
    }

    private void UpdateMoveDirection(Transform target)
    {
        moveDirection = CalculateDirection(transform.position, target.position);

        if (coll.collisions.other != null && coll.collisions.other.tag == "Enemy")
        {
            if (moveDirection.x > moveDirection.y)
            {
                moveDirection.y = moveDirection.y * -1f;
            }
            else if (moveDirection.y > moveDirection.x)
            {
                moveDirection.x = moveDirection.x * -1f;
            }
        }
    }
}

