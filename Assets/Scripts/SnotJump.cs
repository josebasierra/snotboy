using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using System;


public class SnotJump : MonoBehaviour
{
    [SerializeField] Vector2 jumpForce = Vector2.one;
    [SerializeField] float lateralForce = 1f;
    [SerializeField] float extendForce = 1f;

    public event Action OnJump;

    const float cooldownTime = 0.5f;
    bool startJump, extendJump, isOnCooldown;
    Vector2 direction;

    Rigidbody2D myRigidbody;
    Collider2D myCollider;


    void Start()
    {
        isOnCooldown = false;
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
    }


    void Update()
    {
        ReadInput();
    }


    void FixedUpdate()
    {
        bool isOnGround = Physics2DUtility.IsOnGround(transform.position, myCollider);
        bool isAffectedByGravity = myRigidbody.gravityScale > 0;

        // Jump take-off
        if (startJump && (isOnGround || !isAffectedByGravity) && !isOnCooldown)
        {
            Vector2 jumpDirection, finalJumpForce;
            if (isAffectedByGravity)
            {
                jumpDirection = new Vector2(direction.x, 1);
                finalJumpForce = jumpForce;

            }
            else
            {
                jumpDirection = direction;
                if (jumpDirection == Vector2.zero) jumpDirection = Vector2.up;
                finalJumpForce = new Vector2(jumpForce.y, jumpForce.y);
            }

            myRigidbody.AddForce(jumpDirection * finalJumpForce, ForceMode2D.Impulse);
            OnJump?.Invoke();

            isOnCooldown = true;
            Invoke(nameof(EnableJump), cooldownTime);
        }

        // Jump maneuvers
        if (!isOnGround && isAffectedByGravity)
        {
            myRigidbody.AddForce(Vector2.right * direction.x * lateralForce);

            if (extendJump && myRigidbody.velocity.y > 0)
            {
                myRigidbody.AddForce(Vector2.up * extendForce);
            }
        }

        startJump = false;
        extendJump = false;
    }


    public void Jump()
    {
        startJump = true;
    }


    public void ExtendJump()
    {
        extendJump = true;
    }


    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }


    void EnableJump()
    {
        isOnCooldown = false;
    }


    void ReadInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            startJump = true;
        }
        if (Input.GetButton("Jump"))
        {
            extendJump = true;
        }
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
    }
}
