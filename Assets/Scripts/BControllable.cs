using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class BControllable : MonoBehaviour
{
    [SerializeField] Vector2 jumpForce;
    [SerializeField] float jumpCooldown = 1f;

    Rigidbody2D myRigidbody;
    Collider2D myCollider;
    bool canJump = true;


    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
    }


    // Call following methods on 'FixedUpdate' (physics ticks) :

    public virtual void OnLeftKey()
    {
        if (canJump && IsOnGround())
        {
            myRigidbody.AddForce(new Vector2(-1, 1) * jumpForce, ForceMode2D.Impulse);
            canJump = false;
            Invoke("EnableJump", jumpCooldown);
        }
    }


    public virtual void OnRightKey()
    {
        if (canJump && IsOnGround())
        {
            myRigidbody.AddForce(new Vector2(1, 1) * jumpForce, ForceMode2D.Impulse);
            canJump = false;
            Invoke("EnableJump", jumpCooldown);
        }
    }

    //TODO: check if onGround && !isJumping
    public virtual void OnJumpKey()
    {
        //if (IsOnGround())
        //{
        //    myRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        //    Debug.Log("OnGround");
        //}
    }


    public virtual void OnSpecialKey()
    {

    }


    private bool IsOnGround()
    {
        Vector2 currentPosition = transform.position;
        float yOffset = myCollider.bounds.extents.y;

        var hitInfo = Physics2D.Raycast(currentPosition, Vector2.down, yOffset + 0.2f);

        return hitInfo.collider != null;
    }


    private void EnableJump()
    {
        canJump = true;
    }
}

