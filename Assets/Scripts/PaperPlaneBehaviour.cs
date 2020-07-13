using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperPlaneBehaviour : MonoBehaviour
{
    Rigidbody2D myRigidbody;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }


    //TODO: Add  Lateral force depending on inclination
    void FixedUpdate()
    {
        Vector2 faceDirection =  (transform.GetChild(1).position - transform.GetChild(0).position).normalized;
        myRigidbody.AddForce(Vector2.up * Mathf.Abs(faceDirection.y) * Physics.gravity);
    }
}
