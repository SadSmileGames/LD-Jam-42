using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collision2D))]
public class PlayerMotor : MonoBehaviour
{
    private Collision2D collision;

    private void Start()
    {
        collision = GetComponent<Collision2D>();
    }

    public void Move(Vector2 velocity)
    {
        collision.CheckHorizontalCollisions(ref velocity);
        collision.CheckVerticalCollisions(ref velocity);

        transform.Translate(velocity);
    }

    public void Rotate(Transform target, int times, float duration)
    {
        StartCoroutine(Spin(target, times, duration));
    }

    private IEnumerator Spin(Transform target, int times, float duration)
    {
        Vector3 startRotation = target.eulerAngles;
        float targetRotation = startRotation.z + (360.0f * times);
        float t = 0.0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float zRotation = Mathf.Lerp(startRotation.z, targetRotation, t / duration) % (360.0f * times);
            target.eulerAngles = new Vector3(startRotation.x, startRotation.y, -zRotation);

            yield return null;
        }
    }
}
