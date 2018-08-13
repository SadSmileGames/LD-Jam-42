using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //public Transform target;
    //public float speed = 5f;
    //public float stoppingDistance = 1f;

    //private Vector2[] path;
    //private int targetIndex;

    //private bool isFollowing;
    //private float distance;

    ////private void Update()
    ////{
    //    distance = Vector3.Distance(transform.position, target.position);
    //    //if (isFollowing == false && distance > stoppingDistance)
    //    //{
    //    //    PathRequestManager.RequestPath(TransformConversion.Convert2Vector2(transform.position),
    //    //                               TransformConversion.Convert2Vector2(target.position),
    //    //                               OnPathFound);
    //    //}
    //}

    //public void RandomPathing()
    //{
    //    PathRequestManager.RequestRandomPath(TransformConversion.Convert2Vector2(transform.position));
    //}

    //public void OnPathFound(Vector2[] path, bool success)
    //{
    //    if(success)
    //    {
    //        this.path = path;
    //        targetIndex = 0;

    //        if (distance > stoppingDistance)
    //        {
    //            StopCoroutine("FollowPath");
    //            StartCoroutine("FollowPath");
    //        }
    //    }
    //}

    //IEnumerator FollowPath()
    //{
    //    if (path.Length <= 0)
    //    {
    //        isFollowing = false;
    //        yield break;
    //    }

    //    Vector2 currentWaypoint = path[0];
    //    isFollowing = true;

    //    while (isFollowing)
    //    {
    //        if(TransformConversion.Convert2Vector2(transform.position) == currentWaypoint)
    //        {
    //            targetIndex++;

    //            if(targetIndex >= path.Length)
    //            {
    //                isFollowing = false;
    //                yield break;
    //            }
    //            currentWaypoint = path[targetIndex];
    //        }

    //        Vector3 targetPos = TransformConversion.Convert2Vector3(currentWaypoint);

    //        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

    //        if(distance < stoppingDistance)
    //        {
    //            isFollowing = false;
    //            yield break;
    //        }


    //        yield return null;
    //    }
    //}


    public float speed;
    public float stoppingDistance = 3f;
    public float retreatDistance;

    public Transform player;

    private float distance;
    private Vector2 moveDirection;
    private Collision2D coll;

    private Vector2 velocity;

    private float velocityXSmoothing;
    private float velocityYSmoothing;

    private void Start()
    {
        coll = GetComponent<Collision2D>();
        StartCoroutine(FindPlayer());
    }

    private void Update()
    {
        if (player == null)
            return;

        CalculateDistance();
        Move(); 
    }

    private void Move()
    {
        UpdateMoveDirection();
        velocity = moveDirection * speed * Time.deltaTime;

        if (distance > stoppingDistance)
        {
            coll.CheckHorizontalCollisions(ref velocity);
            coll.CheckVerticalCollisions(ref velocity);

            transform.Translate(velocity);
        }
        else if (distance < retreatDistance)
        {
            coll.CheckHorizontalCollisions(ref velocity);
            coll.CheckVerticalCollisions(ref velocity);

            transform.Translate(velocity);
        }
    }

    private IEnumerator FindPlayer()
    {
        if (GameObject.FindGameObjectWithTag("Player").transform != null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
        else
            FindPlayer();

        yield return null;
    }

    private void CalculateDistance()
    {
        distance = Vector2.Distance(transform.position, player.position);
    }

    private Vector2 CalculateDirection (Vector2 from, Vector2 to)
    {
        moveDirection = Vector2.zero;

        Vector2 dir = to - from;
        return dir.normalized;
    }

    private void UpdateMoveDirection ()
    {
        moveDirection = CalculateDirection(transform.position, player.position);

        if(coll.collisions.other != null && coll.collisions.other.tag == "Enemy")
        {
            if(moveDirection.x > moveDirection.y)
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

