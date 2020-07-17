using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;


public class SnotJump : MonoBehaviour
{
    [SerializeField] float jumpForce;

    Rigidbody2D myRigidbody;
    Collider2D myCollider;
    const float jumpCooldown = 0.2f;
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

        myRigidbody.AddForce(jumpDirection.normalized * jumpForce, ForceMode2D.Impulse);
        isJumpOnCooldown = true;
        Invoke("EnableJump", jumpCooldown);
        return true;
    }

    
    public bool IsOnCooldown()
    {
        return isJumpOnCooldown;
    }


    public void DrawTrajectory(Vector2 jumpDirection)
    {

    }


    void EnableJump()
    {
        isJumpOnCooldown = false;
    }
}
