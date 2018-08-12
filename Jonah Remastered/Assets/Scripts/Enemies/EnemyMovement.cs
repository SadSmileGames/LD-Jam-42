using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;

    private Vector2[] path;
    private int targetIndex;

    private bool isFollowing;

    private void Update()
    {
        if(isFollowing == false)
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
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
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

        while (true)
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

            transform.position = Vector3.MoveTowards(transform.position, TransformConversion.Convert2Vector3(currentWaypoint), speed * Time.deltaTime);
            yield return null;
        }
    }
}
