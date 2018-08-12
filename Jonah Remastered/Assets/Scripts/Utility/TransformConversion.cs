using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformConversion : MonoBehaviour
{
    public static Vector2 Convert2Vector2 (Vector3 vector3)
    {
        return new Vector2(vector3.x, vector3.y);
    }

    public static Vector3 Convert2Vector3(Vector2 vector2)
    {
        return new Vector3(vector2.x, vector2.y, 0);
    }
}
