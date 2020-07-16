﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnotController : MonoBehaviour
{
    GameObject controlledObject;
    IMovement controlledMovement;
    IInteractable controlledInteractable;

    bool permeableMode = true;
    bool insideControllableCollider = false;

    //TODO: move jump to another script?
    public Vector2 jumpForce = Vector2.one;
    public float jumpCooldown = 1f;
    bool isJumpOnCooldown = false;

    Highlighter highlighter;


    void Start()
    {
        controlledObject = this.gameObject;
        controlledMovement = controlledObject.GetComponent<IMovement>();
        controlledInteractable = controlledObject.GetComponent<IInteractable>();

        highlighter = GetComponent<Highlighter>();
        SetPermeableMode(permeableMode);

        var cameraController = Camera.main.GetComponent<CameraController>();
        cameraController.SetTarget(controlledObject.transform);
    }


    void Update()
    {
        Vector2 startPosition = controlledObject.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = (mousePosition - startPosition).normalized;


        if (Input.GetButtonDown("Fire2") && !isJumpOnCooldown)
        {
            if (controlledObject != this.gameObject) LeaveObject();
            GetComponent<Rigidbody2D>().AddForce(lookDirection * jumpForce, ForceMode2D.Impulse);
            isJumpOnCooldown = true;
            Invoke("EnableJump", jumpCooldown);
        }

        if (Input.GetKeyDown(KeyCode.F) && controlledObject == this.gameObject)
        {
            SetPermeableMode(!permeableMode);
        }
    }


    void FixedUpdate()
    {
        //Update snot position when inside object (when outside, the 2 positions are the same, nothing happens)
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

    void SetPermeableMode(bool value)
    {
        if (controlledObject != this.gameObject || insideControllableCollider) return;

        permeableMode = value;
        if (permeableMode)
        {

            // set collision layer = IgnoreControllables (layer 9)
            gameObject.layer = 9;

            highlighter?.HighlightOn();
        }
        else
        {
            // set collision layer = Default (layer 0)
            gameObject.layer = 0;

            highlighter?.HighlightOff();
        }
    }


    bool EnterObject(GameObject objectToControl)
    {
        var controllable = objectToControl.GetComponent<Controllable>();
        if (controllable == null || !controllable.IsAvailable()) return false;

        // Update snot body 
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0;

        // Set objectToControl as new object to control
        controlledObject = objectToControl;
        controlledMovement = controlledObject.GetComponent<IMovement>();
        controlledInteractable = controlledObject.GetComponent<IInteractable>();

        controllable.SetIsBeingControlled(true);

        return true;
    }


    bool LeaveObject()
    {
        //TODO: Cache snot body components in case of performance issues

        //Update snot body
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Rigidbody2D>().gravityScale = 1;
        GetComponent<Rigidbody2D>().velocity = controlledObject.GetComponent<Rigidbody2D>().velocity;

        // Leave object
        controlledObject.GetComponent<Controllable>().SetIsBeingControlled(false);

        // Set snot as current object to control
        controlledObject = this.gameObject;
        controlledMovement = this.GetComponent<IMovement>();
        controlledInteractable = this.GetComponent<IInteractable>();

        return true;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (permeableMode && collision.GetComponent<Controllable>())
        {
            insideControllableCollider = true;
            EnterObject(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Controllable>())
        {
            insideControllableCollider = false;
        }
    }


    private void EnableJump()
    {
        isJumpOnCooldown = false;
    }
}
