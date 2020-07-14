using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    IMovement movement;
    IInteractable activable;

    CameraController cameraController;


    void Start()
    {
        movement = GetComponent<IMovement>();
        activable = GetComponent<IInteractable>();

        cameraController = Camera.main.GetComponent<CameraController>();
        cameraController.SetTarget(transform);
    }


    void Update()
    {
        // Special interaction logic
        if (activable != null && Input.GetButton("Special"))
        {
            activable.Interact();
        }

        // Control objects logic
        var castStart = transform.position;
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var castDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        var castDistance = Mathf.Min(Vector2.Distance(castStart, mousePosition), PlayerData.Instance().GetControlReach());

        var intersectedObject = GetIntersectedObject(castStart, castDirection, castDistance);

        if (intersectedObject != null && intersectedObject.GetComponent<Controllable>())
        {
            Highlight(intersectedObject);
            if (Input.GetButtonDown("Fire1")) TakeOver(intersectedObject);
        }

        bool canSurrenderControl = !CompareTag("Player");
        if (canSurrenderControl && Input.GetKeyDown(KeyCode.F))
        {
            SurrenderControl();
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
    }


    GameObject GetIntersectedObject(Vector2 from, Vector2 direction, float distance)
    {
        direction = direction.normalized;
        var hitData = Physics2D.CircleCast(from, 0.05f, direction, distance);

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


    void TakeOver(GameObject objectToControl)
    {
        if (objectToControl == null) return;

        if (this.gameObject == PlayerData.Instance().GetPlayerBody())
        {
            gameObject.SetActive(false);
        }

        objectToControl.AddComponent<PlayerController>();

        Destroy(this);
    }


    void SurrenderControl()
    {
        var currentObjectCollider = gameObject.GetComponent<Collider2D>();
        var currentPosition = transform.position;
        var newPlayerPosition = new Vector2(
            currentPosition.x,
            currentPosition.y + currentObjectCollider.bounds.extents.y
        );

        var playerBody = PlayerData.Instance().GetPlayerBody();
        playerBody.SetActive(true);
        playerBody.transform.position = newPlayerPosition;
        playerBody.AddComponent<PlayerController>();

        Destroy(this);
    }


    void Highlight(GameObject gameobject)
    {
        //hightlight effect (shader, particles, ...)
        Debug.Log($"Highlighting object {gameobject.name}");
    }


    void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.DrawWireSphere(transform.position, PlayerData.Instance().GetControlReach());
        }
    }






}
