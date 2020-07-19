using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Movement
{
    public class LateralMovement : MonoBehaviour, IMovement
    {
        [SerializeField] protected float moveForce = 4f;
        protected Rigidbody2D myRigidbody;
        protected Collider2D myCollider;

        void Start()
        {
            myRigidbody = GetComponent<Rigidbody2D>();
            myCollider = GetComponent<Collider2D>();
        }


        public void Move(Vector2 direction)
        {
            direction.y = 0;
            myRigidbody.AddForce(direction * moveForce);
        }
    }
}
