using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LateralMovement : MonoBehaviour, IMovement
{
    [SerializeField] float moveForce = 4f;
    Rigidbody2D myRigidbody;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }


    public void Move(Vector2 direction)
    {
        direction.y = 0;
        myRigidbody.AddForce(direction * moveForce);
    }
}
