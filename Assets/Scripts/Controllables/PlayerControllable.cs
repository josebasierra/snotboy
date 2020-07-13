using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllable : BaseControllable
{
    [SerializeField] float moveForce = 2f;
    //[SerializeField] float maxSpeed = 2f;

    Rigidbody2D myRigidbody;
    Animator myAnimator;
    bool isTryingToMove = false;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        //var correctedVelocity = myRigidbody.velocity;
        //correctedVelocity.x = Mathf.Min(correctedVelocity.x, maxSpeed);
        //myRigidbody.velocity = correctedVelocity;

        if (!isTryingToMove) 
            myAnimator.SetTrigger("stop");

        isTryingToMove = false;
    }


    public override void OnLeftAction()
    {
        myRigidbody.AddForce(Vector2.left * moveForce);

        //turn left
        var newScale = transform.localScale;
        if (newScale.x > 0) newScale.x *= -1;
        transform.localScale = newScale;

        isTryingToMove = true;
        myAnimator.SetTrigger("move");
    }


    public override void OnRightAction()
    {
        myRigidbody.AddForce(Vector2.right * moveForce);

        //turn right
        var newScale = transform.localScale;
        if (newScale.x < 0) newScale.x *= -1;
        transform.localScale = newScale;

        isTryingToMove = true;
        myAnimator.SetTrigger("move");
    }


}
