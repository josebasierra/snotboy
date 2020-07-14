using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    Animator animator;
    PlayerController playerController;
    Rigidbody2D myRigidbody;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // TODO: Fix animation error when leaving object
    void Update()
    {
        if (playerController == null) return;

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
        

    }
}
