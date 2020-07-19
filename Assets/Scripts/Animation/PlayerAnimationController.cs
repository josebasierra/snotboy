using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class PlayerAnimationController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D myRigidbody;
    Collider2D myCollider;


    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
    }

    // TODO: Fix animation error when leaving object
    void Update()
    {
        var inputDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (inputDirection.x != 0)
        {
            // reverse object
            var newScale = transform.localScale;
            if (Mathf.Sign(newScale.x) != Mathf.Sign(inputDirection.x)) newScale.x *= -1;
            transform.localScale = newScale;

            animator.SetTrigger("move");
        }
        else
        {
            animator.SetTrigger("stop");
        }

        animator.SetBool("isOnGround", Physics2DUtility.IsOnGround(transform.position, myCollider));
        animator.SetFloat("verticalSpeed", myRigidbody.velocity.y);
       
    }
}
