using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
	public AIPath aiPath;
    Animator animator;
    public GameObject parent;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
	{
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            animator.ResetTrigger("Stay");
            animator.SetTrigger("RunLeft");
            animator.ResetTrigger("RunRight");
        }

        else if (aiPath.desiredVelocity.x <= 0.01f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            animator.ResetTrigger("Stay");
            animator.ResetTrigger("RunLeft");
            animator.SetTrigger("RunRight");
        }

        else if (aiPath.desiredVelocity.x == 0f)
        {
            animator.ResetTrigger("RunRight");
            animator.ResetTrigger("RunLeft");
            animator.SetTrigger("Stay");
        }
	}
}
