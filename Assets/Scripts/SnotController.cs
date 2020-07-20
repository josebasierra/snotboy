using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;


public class SnotController : MonoBehaviour
{
    GameObject controlledObject;
    IMovement controlledMovement;
    IInteractable controlledInteractable;

    bool permeableMode = false;
    bool isControllingObject = false; 
    bool insideControllableCollider = false;

    Highlighter highlighter;
    Rigidbody2D myRigidbody;
    SnotJump snotJump;


    public bool IsControllingObject()
    {
        return isControllingObject;
    }


    void Start()
    {
        controlledObject = this.gameObject;
        controlledMovement = controlledObject.GetComponent<IMovement>();
        controlledInteractable = controlledObject.GetComponent<IInteractable>();

        SetPermeableMode(permeableMode);

        var cameraController = Camera.main.GetComponent<CameraController>();
        cameraController.SetTarget(controlledObject.transform);

        myRigidbody = GetComponent<Rigidbody2D>();
        snotJump = GetComponent<SnotJump>();
        highlighter = GetComponent<Highlighter>();
    }


    void Update()
    {
        CheckControlledObject();

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
        CheckControlledObject();

        isControllingObject = (controlledObject != this.gameObject && controlledObject != null);
     
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
        myRigidbody.gravityScale = state ? 1 : 0;
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


    void CheckControlledObject()
    {
        //activate snot if controlledObject is destroyed while controlling it
        if (controlledObject == null)
        {
            ActivateSnot(true);
            TakeOver(this.gameObject);
        }
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
