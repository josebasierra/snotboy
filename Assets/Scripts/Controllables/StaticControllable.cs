using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class StaticControllable : MonoBehaviour, IControllable
{
    [SerializeField] protected Vector2 jumpForce = Vector2.one;
    [SerializeField] protected float jumpCooldown = 0.5f;

    protected Rigidbody2D myRigidbody;
    protected Collider2D myCollider;
    protected bool canMove = true;


    protected void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
    }


    // Call following methods on 'FixedUpdate' (physics ticks) :

    public virtual void OnLeftKey()
    {
        if (canMove && IsOnGround())
        {
            myRigidbody.AddForce(new Vector2(-1, 1) * jumpForce, ForceMode2D.Impulse);
            canMove = false;
            Invoke("EnableMove", jumpCooldown);
        }
    }


    public virtual void OnRightKey()
    {
        if (canMove && IsOnGround())
        {
            myRigidbody.AddForce(new Vector2(1, 1) * jumpForce, ForceMode2D.Impulse);
            canMove = false;
            Invoke("EnableMove", jumpCooldown);
        }
    }


    public virtual void OnJumpKey()
    {
        
    }


    public virtual void OnSpecialKey()
    {

    }


    protected bool IsOnGround()
    {
        Vector2 currentPosition = transform.position;
        float yOffset = myCollider.bounds.extents.y;

        var hitInfo = Physics2D.Raycast(currentPosition, Vector2.down, yOffset + 0.2f);

        return hitInfo.collider != null;
    }


    protected void EnableMove()
    {
        canMove = true;
    }
}

