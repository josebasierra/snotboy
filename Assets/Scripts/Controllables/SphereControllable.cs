using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class SphereControllable : BaseControllable
{
    [SerializeField] float moveForce = 4f;
    Rigidbody2D myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }


    public override void OnLeftKey()
    {
        myRigidbody.AddForce(new Vector2(-1, 1) * moveForce);
    }


    public override void OnRightKey()
    {
        myRigidbody.AddForce(new Vector2(1, 1) * moveForce);
    }
}
