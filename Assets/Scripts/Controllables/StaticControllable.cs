using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class StaticControllable : BaseControllable
{
    [SerializeField] protected Vector2 jumpForce = Vector2.one;
    [SerializeField] protected float jumpCooldown = 0.5f;

    protected Rigidbody2D myRigidbody;
    protected Collider2D myCollider;
    protected bool isJumpInCooldown = false;


    protected void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
    }


    // Call following methods on 'FixedUpdate' (physics ticks) :

    public override void OnLeftAction()
    {
        if (!isJumpInCooldown && IsOnGround())
        {
            myRigidbody.AddForce(new Vector2(-1, 1) * jumpForce, ForceMode2D.Impulse);
            isJumpInCooldown = true;
            Invoke("EnableJump", jumpCooldown);
        }
    }


    public override void OnRightAction()
    {
        if (!isJumpInCooldown && IsOnGround())
        {
            myRigidbody.AddForce(new Vector2(1, 1) * jumpForce, ForceMode2D.Impulse);
            isJumpInCooldown = true;
            Invoke("EnableJump", jumpCooldown);
        }
    }


    protected bool IsOnGround()
    {
        Vector2 currentPosition = transform.position;
        float yOffset = myCollider.bounds.extents.y;

        var hitInfo = Physics2D.Raycast(currentPosition, Vector2.down, yOffset + 0.2f);

        return hitInfo.collider != null;
    }


    protected void EnableJump()
    {
        isJumpInCooldown = false;
    }
}

