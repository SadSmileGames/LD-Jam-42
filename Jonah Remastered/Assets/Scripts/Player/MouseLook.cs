﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Vector2 GetMousePosition()
    {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        return mousePosition;
    }

    public Vector2 GetDirection()
    {
        Vector2 direction = GetMousePosition() - new Vector2(transform.position.x, transform.position.y);
        return direction;
    }

    public float GetAngle(Transform transform)
    {
        float angle = Vector2.Angle(Vector2.right, GetDirection());

        return angle;
    }

    public float GetAngle(Transform transform, bool directional)
    {
        Vector2 direction = GetMousePosition() - new Vector2(transform.position.x, transform.position.y);
        float angle = Vector2.Angle(Vector2.right, direction);

        if (direction.y < 0 && directional)
            return -angle;
        else
            return angle;
    }
}
