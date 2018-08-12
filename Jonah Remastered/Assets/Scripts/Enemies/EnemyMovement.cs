using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    public float stoppingDistance = 1f;

    private Vector2[] path;
    private int targetIndex;

    private bool isFollowing;
    private float distance;

    private void Update()
    {
        distance = Vector3.Distance(transform.position, target.position);
        if (isFollowing == false && distance > stoppingDistance)
        {
            PathRequestManager.RequestPath(TransformConversion.Convert2Vector2(transform.position),
                                       TransformConversion.Convert2Vector2(target.position),
                                       OnPathFound);
        }
    }

    public void OnPathFound(Vector2[] path, bool success)
    {
        if(success)
        {
            this.path = path;
            targetIndex = 0;
            
            if (distance > stoppingDistance)
            {
                StopCoroutine("FollowPath");
                StartCoroutine("FollowPath");
            }
        }
    }

    IEnumerator FollowPath()
    {
        if (path.Length <= 0)
        {
            isFollowing = false;
            yield break;
        }

        Vector2 currentWaypoint = path[0];
        isFollowing = true;

        while (isFollowing)
        {
            if(TransformConversion.Convert2Vector2(transform.position) == currentWaypoint)
            {
                targetIndex++;

                if(targetIndex >= path.Length)
                {
                    isFollowing = false;
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            Vector3 targetPos = TransformConversion.Convert2Vector3(currentWaypoint);

            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

            if(distance < stoppingDistance)
            {
                isFollowing = false;
                yield break;
            }
                

            yield return null;
        }
    }
}
