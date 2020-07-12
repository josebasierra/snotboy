using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] float ControlReach = 4f;

    private Controllable controllable;
    private CameraController cameraController;
    private bool canSurrenderControl = false;

    private void Start()
    {
        controllable = Player.GetComponent<Controllable>();
        cameraController = Camera.main.GetComponent<CameraController>();
        cameraController.SetTarget(controllable.gameObject.transform);
    }

    private void Update()
    {
        var mouseControllable = TryGetControllableOnMousePosition();
        if (mouseControllable != null)
        {
            HighlightControllable(mouseControllable);

            if (Input.GetMouseButtonDown(0))
            {
                TakeOverControllable(mouseControllable);
            }
        }

        if (canSurrenderControl && Input.GetKeyDown(KeyCode.F))
        {
            SurrenderControl();
        }
    }

    //TODO: Get input in Update method and then process it in FixedUpdate (if problems with input delay are noticeable)
    private void FixedUpdate()
    {
        var horizontalValue = Input.GetAxis("Horizontal");

        if (horizontalValue < 0) controllable.OnLeftKey();
        if (horizontalValue > 0) controllable.OnRightKey();
        if (Input.GetButton("Jump")) controllable.OnJumpKey();
        if (Input.GetButton("Special")) controllable.OnSpecialKey();
    }

    protected virtual void TakeOverControllable(Controllable controllable)
    {
        // Hide player if currently under control
        if (this.controllable.gameObject == Player)
        {
            Player.SetActive(false);
        }
        
        // Set new controller
        this.controllable = controllable;
        cameraController.SetTarget(controllable.gameObject.transform);

        canSurrenderControl = true;
    }

    protected virtual void SurrenderControl()
    {
        var currentObjectCollider = controllable.gameObject.GetComponent<Collider2D>();
        var currentPostion = controllable.transform.position;
        var newPlayerPosition = new Vector2(
            currentPostion.x,
            currentPostion.y + currentObjectCollider.bounds.extents.y
        );
        
        Player.SetActive(true);
        Player.transform.position = newPlayerPosition;
        controllable = Player.GetComponent<Controllable>();
        cameraController.SetTarget(Player.transform);
        
        canSurrenderControl = false;
    }

    private void HighlightControllable(Controllable ctrl)
    {
        Debug.DrawLine(controllable.gameObject.transform.position, ctrl.transform.position, Color.green);
        Debug.Log($"Highlighting object {ctrl.gameObject.name}");
    }

    protected virtual void OnDrawGizmos()
    {
        var position = controllable?.gameObject.transform.position ?? Player.transform.position;
        Gizmos.DrawWireSphere(position, ControlReach);
    }

    private Controllable TryGetControllableOnMousePosition()
    {
        bool InReach(Controllable other)
        {
            var playerPosition = controllable.gameObject.transform.position;
            var distance = Vector3.Distance(other.transform.position, playerPosition);
            return distance < ControlReach;
        }
   
        // Find Controllables in reach
        var controllables = FindObjectsOfType<Controllable>()
            .Where(c => c != controllable)
            .Where(InReach);

        // Check mouse is over
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return controllables
            .Where(c => c.gameObject.GetComponent<Collider2D>() != null)
            .FirstOrDefault(c => c.gameObject.GetComponent<Collider2D>().OverlapPoint(mousePos));
    }
}
