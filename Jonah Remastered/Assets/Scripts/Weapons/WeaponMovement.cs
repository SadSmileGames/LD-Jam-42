using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMovement : MonoBehaviour
{
    public float smoothAmount = 25f;
    public Vector2 offset;

    private Transform player;
    private MouseLook mouseLook;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        mouseLook = GetComponent<MouseLook>();
    }

    private void Update()
    {
        transform.right = mouseLook.GetDirection();
    }

    private void LateUpdate()
    {
        Vector2 targetPosition = new Vector2 (player.position.x, player.position.y) + offset;
        Vector2 smoothPosition = Vector2.Lerp(transform.position, targetPosition, smoothAmount * Time.deltaTime);
        transform.position = smoothPosition;
    }
}
