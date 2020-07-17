using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Movement
{
    public class RotationMovement : MonoBehaviour, IMovement
    {
        [SerializeField] float rotationForce = 80f;
        [SerializeField] float maxAngularSpeed = 10f;
        [SerializeField] Transform rotationCenter;

        Rigidbody2D myRigidbody;


        void Start()
        {
            myRigidbody = GetComponent<Rigidbody2D>();
        }


        public void Move(Vector2 direction)
        {
            if (direction.x > 0) direction.x = 1;
            else if (direction.x < 0) direction.x = -1;

            // Cross product to know force direction
            Vector3 a = transform.position - rotationCenter.position;
            Vector3 b = new Vector3(0, 0, 1);
            Vector3 forceDirection = Vector3.Cross(a, b) * direction.x;

            Vector3 forcePosition = transform.position + a;
            myRigidbody.AddForceAtPosition(forceDirection * rotationForce,forcePosition);

            float currentAngularSpeed = myRigidbody.angularVelocity;
            myRigidbody.angularVelocity = Mathf.Clamp(currentAngularSpeed, -maxAngularSpeed, maxAngularSpeed);

            Debug.DrawLine(forcePosition, forcePosition + forceDirection, Color.green);
        }
    }
}

