using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Material HighlightMaterial;
    [SerializeField] Material DefaultObjectMaterial;

    IMovement movement;
    IInteractable activable;

    CameraController cameraController;
    GameObject highlighted;

    void Start()
    {
        movement = GetComponent<IMovement>();
        activable = GetComponent<IInteractable>();
        cameraController = Camera.main.GetComponent<CameraController>();
        cameraController.SetTarget(transform);
    }


    void Update()
    {
        if (highlighted != null) DeHighlightControllable(highlighted);

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
            HighlightControllable(intersectedObject);
            highlighted = intersectedObject;
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


    private void HighlightControllable(GameObject ctrl)
    {
        ctrl.GetComponent<SpriteRenderer>().material = HighlightMaterial;

        //Debug.DrawLine(gameObject.transform.position, ctrl.transform.position, Color.green);
        Debug.Log($"Highlighting object {ctrl.gameObject.name}");
    }


    private void DeHighlightControllable(GameObject ctrl)
    {
        if (ctrl == null) return;
        ctrl.GetComponent<SpriteRenderer>().material = DefaultObjectMaterial;
        Debug.Log($"De-Highlighting object {ctrl.gameObject.name}");
    }


    protected virtual void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.DrawWireSphere(transform.position, PlayerData.Instance().GetControlReach());
        }
    }



}
