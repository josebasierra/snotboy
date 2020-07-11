using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class BControllable : MonoBehaviour
{
    [SerializeField] float lateralForce;
    [SerializeField] float jumpForce;

    Rigidbody2D myRigidbody;
    Collider2D myCollider;


    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
    }

    
    // Call following methods on 'FixedUpdate' (physics ticks) :

    public virtual void OnLeftKey()
    {
        myRigidbody.AddForce(Vector2.left * lateralForce);
    }


    public virtual void OnRightKey()
    {
        myRigidbody.AddForce(Vector2.right * lateralForce);
    }

    //TODO: check if onGround && !isJumping
    public virtual void OnJumpKey() 
    {
        Vector2 currentPosition = transform.position;
        float yOffset = myCollider.bounds.extents.y;

        var hitInfo = Physics2D.Raycast(currentPosition, Vector2.down, yOffset + 0.05f);

       
        if (hitInfo.collider != null)
        {
            myRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }


    public virtual void OnSpecialKey()
    {

    }
}
