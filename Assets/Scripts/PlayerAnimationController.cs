using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        animator.SetBool("isOnGround", IsOnGround(myCollider));
        animator.SetFloat("verticalSpeed", myRigidbody.velocity.y);
        

    }

    bool IsOnGround(Collider2D collider)
    {
        Vector2 currentPosition = transform.position;
        float yOffset = collider.bounds.extents.y;

        var hitInfo = Physics2D.Raycast(currentPosition, Vector2.down, yOffset + 0.2f);

        return hitInfo.collider != null;
    }
}
