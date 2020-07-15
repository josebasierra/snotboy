using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnotController : MonoBehaviour
{
    GameObject controlledObject;
    IMovement controlledMovement;
    IInteractable controlledInteractable;

    //TODO: Implement permeable mode (different effect), only enter objects if in permeable mode
    bool permeableMode = true;


    void Start()
    {
        controlledObject = this.gameObject;
        controlledMovement = controlledObject.GetComponent<IMovement>();
        controlledInteractable = controlledObject.GetComponent<IInteractable>();

        var cameraController = Camera.main.GetComponent<CameraController>();
        cameraController.SetTarget(controlledObject.transform);
    }


    void Update()
    {
        Vector2 startPosition = controlledObject.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = (mousePosition - startPosition).normalized;


        if (Input.GetButtonDown("Fire2"))
        {
            if (controlledObject != this.gameObject) LeaveObject();
            GetComponent<Rigidbody2D>().AddForce(lookDirection, ForceMode2D.Impulse);
        }
    }


    void FixedUpdate()
    {
        //snot position inside object
        this.transform.position = controlledObject.transform.position;

        // Movement logic
        if (controlledMovement != null)
        {
            var xDirection = Input.GetAxis("Horizontal");
            var yDirection = Input.GetAxis("Vertical");
            var moveDirection = new Vector2(xDirection, yDirection);
            controlledMovement.Move(moveDirection);
        }

        // Special interaction logic
        if (controlledInteractable != null && Input.GetButton("Special"))
        {
            controlledInteractable.Interact();
        }
    }


    bool EnterObject(GameObject objectToControl)
    {
        var controllable = objectToControl.GetComponent<Controllable>();
        if (controllable == null || !controllable.IsAvailable()) return false;

        // Set objectToControl as new object to control
        controlledObject = objectToControl;
        controlledMovement = controlledObject.GetComponent<IMovement>();
        controlledInteractable = controlledObject.GetComponent<IInteractable>();

        controllable.SetIsBeingControlled(true);

        // Update snot body 
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        return true;
    }


    bool LeaveObject()
    {   
        // Check if there space to leave object 
        Vector2 castStart = controlledObject.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 castDirection = (mousePosition - castStart).normalized;
        Vector2 objectUnderControlExtents = controlledObject.GetComponent<Collider2D>().bounds.extents;

        var hitData = Physics2D.CircleCast(castStart, 0.1f, castDirection, objectUnderControlExtents.magnitude);
        if (hitData.collider != null && hitData.collider.gameObject.GetComponent<Controllable>() == null) return false;

        // Leave object
        controlledObject.GetComponent<Controllable>().SetIsBeingControlled(false);

        // Set snot as current object to control
        controlledObject = this.gameObject;
        controlledMovement = this.GetComponent<IMovement>();
        controlledInteractable = this.GetComponent<IInteractable>();

        // Update snot body
        Vector2 objectUnderControlPosition = controlledObject.transform.position;
        var placePosition = objectUnderControlPosition + castDirection * objectUnderControlExtents.magnitude;

        transform.position = placePosition;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;

        return true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (permeableMode)
            EnterObject(collision.gameObject);
    }
}
