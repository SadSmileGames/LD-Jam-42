using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEffectController : MonoBehaviour
{
    public GameObject graphics;

    private Animator animator;

    private void Start()
    {
        animator = graphics.GetComponent<Animator>();
    }


}
