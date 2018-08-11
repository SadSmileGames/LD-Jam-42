using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectsController : MonoBehaviour
{
    public GameObject graphics;

    private MouseLook mouseLook;
    private PlayerController controller;
    private Animator animator;

    void Start ()
    {
        if (graphics == null)
            Debug.LogError("There is no graphics object asigned to this script!!!");

        mouseLook = this.GetComponent<MouseLook>();
        controller = this.GetComponent<PlayerController>();
        animator = graphics.GetComponent<Animator>();
	}

	void Update ()
    {
        CheckDirection();
        CheckIfMoving();
	}

    private void CheckDirection()
    {
        float angle = mouseLook.GetAngle(this.transform, true);

        if(angle <= 45 && angle >= -45)
        {
            ResetDirection();
            animator.SetBool("facingRight", true);
        }

        if (angle >= 45 && angle <= 135)
        {
            ResetDirection();
            animator.SetBool("facingUp", true);
        }

        if (angle > 135 || angle < -135)
        {
            ResetDirection();
            animator.SetBool("facingLeft", true);
        }

        if (angle <= -45 && angle >= -135)
        {
            ResetDirection();
            animator.SetBool("facingDown", true);
        }
    }

    private void ResetDirection()
    {
        animator.SetBool("facingRight", false);
        animator.SetBool("facingLeft", false);
        animator.SetBool("facingUp", false);
        animator.SetBool("facingDown", false);
    }

    private void CheckIfMoving()
    {
        if(controller.IsMoving())
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    public void StartSpinning()
    {
        StartCoroutine(Spin(graphics.transform, 1, controller.dashTime));
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
