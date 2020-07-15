using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float controlReach = 4f;
    [SerializeField] GameObject originalBody;
    [SerializeField] Material highlightMaterial;
    [SerializeField] Transform playerLight;

    GameObject objectUnderControl;
    IMovement movement;
    IInteractable interactable;

    CameraController cameraController;

    //TODO: Create highlight component responsible of highlighting an object?
    GameObject highlighted;
    Material originalMaterial;


    void Start()
    {
        objectUnderControl = originalBody;
        movement = objectUnderControl.GetComponent<IMovement>();
        interactable = objectUnderControl.GetComponent<IInteractable>();

        cameraController = Camera.main.GetComponent<CameraController>();
        cameraController.SetTarget(objectUnderControl.transform);
    }


    void Update()
    {
        // Control objects logic

        DeHighlightControllable(highlighted);

        Vector2 castStart = objectUnderControl.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 castDirection = (mousePosition - castStart).normalized;
        float castDistance = Mathf.Min(Vector2.Distance(castStart, mousePosition), controlReach);

        var intersectedObject = GetIntersectedObject(castStart, castDirection, castDistance);

        bool controllableObjectFound = intersectedObject != null && intersectedObject.GetComponent<Controllable>();

        if (controllableObjectFound)
        {
            HighlightControllable(intersectedObject);
        }

        if (Input.GetButtonDown("Fire1") && controllableObjectFound)
        {
            TakeOver(intersectedObject);
        }
        
        if (Input.GetButtonDown("Fire2") && !objectUnderControl.CompareTag("Player"))
        {
            Vector2 objectUnderControlExtents = objectUnderControl.GetComponent<Collider2D>().bounds.extents;
            var hitData = Physics2D.CircleCast(castStart, 0.1f, castDirection, objectUnderControlExtents.magnitude);

            if (hitData.collider == null)
            {
                Vector2 objectUnderControlPosition = objectUnderControl.transform.position;
                var placePosition = objectUnderControlPosition + castDirection * objectUnderControlExtents.magnitude;
                ActivateAndPlaceObject(originalBody, placePosition);

                TakeOver(originalBody);
            }
        }
    }


    void FixedUpdate()
    {
        // Movement logic
        if (movement != null)
        {
            var xDirection = Input.GetAxis("Horizontal");
            var yDirection = Input.GetAxis("Vertical");
            var moveDirection = new Vector2(xDirection, yDirection);
            movement.Move(moveDirection);
        }

        // Special interaction logic
        if (interactable != null && Input.GetButton("Special"))
        {
            interactable.Interact();
        }
    }


    GameObject GetIntersectedObject(Vector2 from, Vector2 direction, float distance)
    {
        direction = direction.normalized;
        //var hitData = Physics2D.CircleCast(from, 0.05f, direction, distance);
        var hitData = Physics2D.Raycast(from, direction, distance);

        if (hitData.collider != null)
        {
            Debug.DrawLine(from, hitData.point, Color.red);
            return hitData.collider.gameObject;
        }
        else
        {
            Debug.DrawLine(from, from + direction.normalized * distance, Color.green);
            return null;
        }
    }


    void ActivateAndPlaceObject(GameObject objectToPlace, Vector2 targetPosition)
    {
        if (objectToPlace == null) return;

        objectToPlace.SetActive(true);
        objectToPlace.transform.position = targetPosition;
    }


    void TakeOver(GameObject objectToControl)
    {
        if (objectToControl == null) return;

        if (objectUnderControl == originalBody)
        {
            objectUnderControl.SetActive(false);
        }

        objectUnderControl = objectToControl;
        movement = objectUnderControl.GetComponent<IMovement>();
        interactable = objectUnderControl.GetComponent<IInteractable>();

        //update light position
        if (playerLight != null)
        {
            playerLight.transform.position = objectUnderControl.transform.position;
            playerLight.parent = objectUnderControl.transform;
        }

        //update camera position
        cameraController.SetTarget(objectUnderControl.transform);
    }


    void SurrenderControl()
    {
        var currentObjectCollider = objectUnderControl.GetComponent<Collider2D>();
        var currentPosition = transform.position;
        var newPlayerPosition = new Vector2(
            currentPosition.x,
            currentPosition.y + currentObjectCollider.bounds.extents.y
        );

        Debug.Log(currentObjectCollider.bounds.extents.y);

        originalBody.SetActive(true);
        originalBody.transform.position = newPlayerPosition;
    }


    private void HighlightControllable(GameObject ctrl)
    {
        originalMaterial = ctrl.GetComponent<SpriteRenderer>().material;
        ctrl.GetComponent<SpriteRenderer>().material = highlightMaterial;

        //Debug.Log($"Highlighting object {ctrl.gameObject.name}");

        highlighted = ctrl;
    }


    private void DeHighlightControllable(GameObject ctrl)
    {
        if (ctrl == null) return;
        ctrl.GetComponent<SpriteRenderer>().material = originalMaterial;
        //Debug.Log($"De-Highlighting object {ctrl.gameObject.name}");

        highlighted = null;
    }


    protected virtual void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.DrawWireSphere(objectUnderControl.transform.position, controlReach);
        }
    }
}
