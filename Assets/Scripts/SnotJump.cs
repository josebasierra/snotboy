using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;


public class SnotJump : MonoBehaviour
{
    [SerializeField] float jumpForce;


    [SerializeField] float jumpCooldown = 0.4f;
    [SerializeField] float zeroGravityTime = 0.1f;

    Rigidbody2D myRigidbody;
    Collider2D myCollider;

    bool isJumpOnCooldown;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
    }


    public bool Jump(Vector2 jumpDirection, bool checkGround = true)
    {
        if (isJumpOnCooldown) return false;
        if (checkGround && !Physics2DUtility.IsOnGround(transform.position, myCollider)) return false;

        myRigidbody.gravityScale = 0;
        Invoke("EnableGravity", zeroGravityTime);

        myRigidbody.AddForce(jumpDirection.normalized * jumpForce, ForceMode2D.Impulse);

        isJumpOnCooldown = true;
        Invoke("EnableJump", jumpCooldown);

        return true;
    }

    
    public bool IsOnCooldown()
    {
        return isJumpOnCooldown;
    }


    void EnableJump()
    {
        isJumpOnCooldown = false;
        myRigidbody.gravityScale = 1;
    }


    void EnableGravity()
    {
        myRigidbody.gravityScale = 1;
    }
}
