using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingMovement : MonoBehaviour, IMovement
{
    [SerializeField] Vector2 jumpForce = Vector2.one;
    [SerializeField] float jumpCooldown = 0.5f;

    Rigidbody2D myRigidbody;
    Collider2D myCollider;
    bool isJumpInCooldown = false;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
    }


    public void Move(Vector2 direction)
    {
        if (direction.x != 0 && !isJumpInCooldown && IsOnGround())
        {
            myRigidbody.AddForce(new Vector2(1 * Mathf.Sign(direction.x), 1) * jumpForce, ForceMode2D.Impulse);
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
