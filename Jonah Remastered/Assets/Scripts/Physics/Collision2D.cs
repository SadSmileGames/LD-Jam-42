using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is responsible for raycast collisions

[RequireComponent(typeof(BoxCollider2D))]
public class Collision2D : MonoBehaviour
{
    public float skinWidth = 0.015f;

    public float distanceBentweenRays = 0.1f;
    public LayerMask collisionMask;

    private BoxCollider2D coll;
    private RaycastOrigins raycastOrigins;

    private int rayCountX = 4;
    private int rayCountY = 4;

    private float horizontalRaySpacing;
    private float verticalRaySpacing;

    void Start()
    {
        raycastOrigins = new RaycastOrigins();
        coll = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();
    }

    public void CheckHorizontalCollisions(ref Vector2 velocity)
    {
        UpdateRaycastOrigins();

        float directionX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;

        for (int i = 0; i < rayCountX; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);

            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX * 2, Color.red);

            if (hit2D)
            {
                velocity.x = (hit2D.distance - skinWidth) * directionX;
                rayLength = hit2D.distance;
            }

            
        }
    }

    public void CheckVerticalCollisions(ref Vector2 velocity)
    {
        UpdateRaycastOrigins();

        float directionY = Mathf.Sign (velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;
        
        for (int i = 0; i < rayCountY; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
            Debug.DrawRay(rayOrigin, Vector2.up * directionY * 2, Color.red);

            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

            if(hit2D)
            {
                velocity.y = (hit2D.distance - skinWidth) * directionY;
                rayLength = hit2D.distance;
            }
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

        float boundsWidth = bounds.size.x;
        float boundsHeight = bounds.size.y;

        rayCountX = Mathf.RoundToInt(boundsHeight / distanceBentweenRays);
        rayCountY = Mathf.RoundToInt(boundsWidth / distanceBentweenRays);

        horizontalRaySpacing = bounds.size.y / (rayCountX - 1);
        verticalRaySpacing = bounds.size.x / (rayCountY - 1);
    }

    private Bounds GetBounds () {
        Bounds bounds = coll.bounds;

        bounds = coll.bounds;
        bounds.Expand(skinWidth * -2);

        return bounds;
    }
}
