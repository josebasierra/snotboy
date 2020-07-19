using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Animation
{
    public class RocketAnimationController : MonoBehaviour
    {
        private Animator animator;
        private Collider2D myCollider;
        private Rigidbody2D myRigidbody;

        private void Start()
        {
            animator = GetComponent<Animator>();
            myCollider = GetComponent<Collider2D>();
            myRigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            var yVelocity = myRigidbody.velocity.y;
            animator.SetBool("rocketOn", yVelocity > 0);
        }
    }
}