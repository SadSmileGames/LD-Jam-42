using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private EnemyMovement movement;
    private float idleTime = 3f;

    public enum State
    {
        Idle,
        Chasing, 
        Shooting
    }

    public State state;

    private void Start()
    {
        movement = GetComponent<EnemyMovement>();
        state = State.Idle;
    }

    private void Update()
    {
        switch(state)
        {
            case State.Idle:
                Idle();
                break;
            case State.Chasing:
                Chasing();
                break;
            case State.Shooting:
                Shooting();
                break;
            default:
                Debug.Log("?");
                break;
        }
    }

    private void Idle()
    {
    }

    private void Chasing()
    {

    }

    private void Shooting()
    {

    }
}
