using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PendulumControllable : MonoBehaviour, IControllable
{
    [SerializeField] float moveForce = 4f;
    
    Rigidbody2D myRigidbody;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }


    public void OnLeftKey()
    {
        myRigidbody.AddRelativeForce(new Vector2(-1, 1) * moveForce);
    }


    public void OnRightKey()
    {
        myRigidbody.AddRelativeForce(new Vector2(1, 1) * moveForce);
    }


    public void OnSpecialKey()
    {
        throw new System.NotImplementedException();
    }

    public void OnJumpKey()
    {
        throw new System.NotImplementedException();
    }
}
