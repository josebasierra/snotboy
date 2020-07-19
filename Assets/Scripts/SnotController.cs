using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;


public class SnotController : MonoBehaviour
{
    GameObject controlledObject;
    IMovement controlledMovement;
    IInteractable controlledInteractable;

    SnotJump snotJump;

    bool permeableMode = false;
    bool isControllingObject = false; 
    bool insideControllableCollider = false;

    Highlighter highlighter;


    void Start()
    {
        controlledObject = this.gameObject;
        controlledMovement = controlledObject.GetComponent<IMovement>();
        controlledInteractable = controlledObject.GetComponent<IInteractable>();

        snotJump = GetComponent<SnotJump>();

        highlighter = GetComponent<Highlighter>();
        SetPermeableMode(permeableMode);

        var cameraController = Camera.main.GetComponent<CameraController>();
        cameraController.SetTarget(controlledObject.transform);
    }


    void Update()
    {
        // snot jump
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = (mousePosition - (Vector2)transform.position).normalized;
        if (Input.GetButtonDown("Fire2"))
        {
            if (isControllingObject && !snotJump.IsOnCooldown())
            {
                LeaveControllableObject();
            }
            bool needsGroundToJump = !isControllingObject;
            snotJump.Jump(lookDirection, needsGroundToJump);
        }

        // permeable mode
        if (Input.GetKeyDown(KeyCode.F) && !isControllingObject)
        {
            SetPermeableMode(!permeableMode);
        }
    }


    void FixedUpdate()
    {       
        this.transform.position = controlledObject.transform.position;
        isControllingObject = (controlledObject != this.gameObject);

        //activate snot if controlledObject is destroyed while controlling it
        if (isControllingObject && controlledObject == null)
        {
            ActivateSnot(true);
            TakeOver(this.gameObject);
        }

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
        if (isControllingObject || insideControllableCollider) return;

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


    void ActivateSnot(bool state)
    {
        GetComponent<SpriteRenderer>().enabled = state;
        GetComponent<Rigidbody2D>().gravityScale = state? 1 : 0;

        // enabling colliders can cause snot to leave the map
        //GetComponent<Collider2D>().enabled = state;
        //transform.GetChild(0).GetComponent<Collider2D>().enabled = state;
    }


    void TakeOver(GameObject objectToControl)
    {
        controlledObject = objectToControl.gameObject;
        controlledMovement = objectToControl.GetComponent<IMovement>();
        controlledInteractable = objectToControl.GetComponent<IInteractable>();
    }


    void EnterControllableObject(Controllable controllable)
    {
        ActivateSnot(false);
        controllable.SetIsBeingControlled(true);

        TakeOver(controllable.gameObject);
    }


    void LeaveControllableObject()
    {
        ActivateSnot(true);
        GetComponent<Rigidbody2D>().velocity = controlledObject.GetComponent<Rigidbody2D>().velocity;

        controlledObject.GetComponent<Controllable>().SetIsBeingControlled(false);

        TakeOver(this.gameObject);
    }


    void OnTriggerStay2D(Collider2D collision)
    {
        var controllable = collision.GetComponent<Controllable>();
        if (permeableMode && !isControllingObject && controllable != null && controllable.IsAvailable())
        {
            EnterControllableObject(controllable);
        }
        if (controllable != null)
        {
            insideControllableCollider = true;
        }
    }


    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Controllable>())
        {
            insideControllableCollider = false;
        }
    }
}
