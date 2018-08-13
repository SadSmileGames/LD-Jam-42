using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject graphics;

    public float attackDuratrion = 1f;
    public Vector2 chaseTime = new Vector2(1, 2);
    public Vector2 idleTime = new Vector2(0.5f, 1);

    private EnemyMovement movement;
    private Weapon weapon;

    private float chaseTimeRemaining;
    private float idleTimeRemaining;
    private float attackTimeRemaining;

    private Transform player;
    private Transform targetLocation;

    private bool isPlayerDead;
    private Animator animator;

    public enum State
    {
        Idle,
        Chasing, 
        Attacking
    }

    public State state;

    private void OnEnable()
    {
        EnemyMovement.OnTargetReachedEvent += ReachedTarget;
        PlayerHealth.OnPlayerDeath += SetIdlePermanently;
    }

    private void OnDisable()
    {
        EnemyMovement.OnTargetReachedEvent -= ReachedTarget;
        PlayerHealth.OnPlayerDeath -= SetIdlePermanently;
    }

    private void Start()
    {
        weapon = GetComponent<Weapon>();
        animator = graphics.GetComponent<Animator>();

        if(GameObject.FindGameObjectWithTag("Player") != null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        movement = GetComponent<EnemyMovement>();
        state = State.Idle;
        idleTimeRemaining = 2f;


        chaseTimeRemaining = Random.Range(chaseTime.x, chaseTime.y);
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
            case State.Attacking:
                Attacking();
                break;
            default:
                Debug.Log("?");
                break;
        }
    }

    private void Idle()
    {
        if (idleTimeRemaining <= 0)
        {
            SelectRandomState();
            idleTimeRemaining = Random.Range(idleTime.x, idleTime.y);
        }

        idleTimeRemaining -= Time.deltaTime;
    }

    private void Chasing()
    {
        if (state != State.Chasing || player == null)
            return;
        
        if (chaseTimeRemaining <= 0)
        {
            SelectRandomState();
            chaseTimeRemaining = Random.Range(chaseTime.x, chaseTime.y);
        }

        chaseTimeRemaining -= Time.deltaTime;

        movement.Move(player);     
    }

    public void ReachedTarget(Transform actor)
    {
        if(actor == this.transform)
            state = State.Attacking;
    }

    private void Attacking()
    {
        if (state != State.Attacking || player == null)
            return;

        if(attackTimeRemaining <= 0)
        {
            SelectRandomState();
            attackTimeRemaining = attackDuratrion;
        }

        Vector2 dir = player.position - transform.position;

        weapon.SetFirePointDirection(dir);
        weapon.Shoot();

        animator.SetBool("facingRight", false);
        animator.SetBool("facingLeft", false);
        animator.SetBool("facingUp", false);
        animator.SetBool("facingDown", false);

        if (dir.x > dir.y)
        {
            if (dir.x >= 0)
                animator.SetBool("facingRight", true);
            else
                animator.SetBool("facingLeft", true);
        }
        else
        {
            if (dir.y >= 0)
                animator.SetBool("facingUp", true);
            else
                animator.SetBool("facingDown", true);
        }

        animator.SetTrigger("Shooting");



        attackTimeRemaining -= Time.deltaTime;
        
    }

    private void SelectRandomState()
    {
        int newState = Random.Range(1, 4);

        if(isPlayerDead)
        {
            state = State.Idle;
            return;
        }

        if (newState == 1)
            state = State.Idle;
        else if (newState == 2)
            state = State.Chasing;
        else if (newState == 3)
            state = State.Attacking;
    }

    public void SetIdlePermanently()
    {
        isPlayerDead = true;
    }
}
