using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Movement
{
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
            if (direction.x != 0 && !isJumpInCooldown && Physics2DUtility.IsOnGround(transform.position, myCollider))
            {
                myRigidbody.AddForce(new Vector2(1 * Mathf.Sign(direction.x), 1) * jumpForce, ForceMode2D.Impulse);
                isJumpInCooldown = true;
                Invoke("EnableJump", jumpCooldown);
            }
        }


        void EnableJump()
        {
            isJumpInCooldown = false;
        }
    }
}
