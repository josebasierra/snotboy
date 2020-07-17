using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class HelicopterAnimationController : MonoBehaviour
{
    [SerializeField] float VelocityFlipTrigger = 0.5f;
    
    private Animator animator;
    private Collider2D myCollider;
    private Rigidbody2D myRigidbody;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        var xVelocity = myRigidbody.velocity.x;

        if (Math.Abs(xVelocity) > VelocityFlipTrigger)
        {
            // reverse object
            var newScale = transform.localScale;
            if (Mathf.Sign(newScale.x) != Mathf.Sign(xVelocity)) newScale.x *= -1;
            transform.localScale = newScale;
        }

        animator.SetBool("isOnGround", Physics2DUtility.IsOnGround(transform.position, myCollider));
    }
}