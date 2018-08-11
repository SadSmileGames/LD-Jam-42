using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is responsible for raycast collisions

[RequireComponent(typeof(BoxCollider2D))]
public class Collision2D : MonoBehaviour
{
    const float skinWidth = 0.015f;

    public LayerMask collisionMask;
    public Vector2 rayCount = new Vector2(4f, 4f);

    private BoxCollider2D coll;
    private RaycastOrigins raycastOrigins;
    private Vector2 raySpacing;

    void Start()
    {
        raycastOrigins = new RaycastOrigins();
        coll = GetComponent<BoxCollider2D>();
        
    }

    public void CheckHorizontalCollisions(ref Vector2 velocity)
    {
        //UpdateRaycastOrigins();

        float directionX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;

        for (int i = 0; i < rayCount.x; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (raySpacing.x * i);

            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            if (hit2D)
            {
                velocity.x = (hit2D.distance - skinWidth) * directionX;
                rayLength = hit2D.distance;
            }

            Debug.DrawRay(rayOrigin, Vector2.up * directionX * rayLength, Color.red);
        }
    }

    public void CheckVerticalCollisions(ref Vector2 velocity)
    {
        //UpdateRaycastOrigins();

        float directionY = Mathf.Sign (velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;

        for (int i = 0; i < rayCount.y; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (raySpacing.y * i + velocity.x);

            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

            if(hit2D)
            {
                velocity.y = (hit2D.distance - skinWidth) * directionY;
                rayLength = hit2D.distance;
            }

            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);
        }
    }

    public void UpdateRaycastOrigins()
    {
        Bounds bounds = GetBounds();

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    private void CalculateRaySpacing()
    {
        Bounds bounds = GetBounds();

        raySpacing.x = Mathf.Clamp(rayCount.x, 2, int.MaxValue);
        raySpacing.y = Mathf.Clamp(rayCount.y, 2, int.MaxValue);

        raySpacing.x = bounds.size.y / (rayCount.x - 1);
        raySpacing.y = bounds.size.x / (rayCount.y - 1);
    }

    private Bounds GetBounds () {
        Bounds bounds = coll.bounds;

        bounds = coll.bounds;
        bounds.Expand(skinWidth * -2);

        return bounds;
    }
}
