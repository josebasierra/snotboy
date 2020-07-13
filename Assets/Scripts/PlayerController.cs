using Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] public GameObject Player;
    [SerializeField] public float ControlReach = 4f;
    [SerializeField] public Material HighlightMaterial;
    [SerializeField] public Material DefaultObjectMaterial;

    private BaseControllable controllable;
    private BaseControllable highlightedControllable;
    private CameraController cameraController;
    private bool canSurrenderControl = false;

    private void Start()
    {
        controllable = Player.GetComponent<BaseControllable>();
        cameraController = Camera.main.GetComponent<CameraController>();
        cameraController.SetTarget(controllable.gameObject.transform);
    }

    private void Update()
    {
        var mouseControllable = TryGetControllableOnMousePosition();
        if (mouseControllable != null)
        {
            if (highlightedControllable != null && highlightedControllable != mouseControllable) 
                DeHighlightControllable(highlightedControllable);
            if (mouseControllable != highlightedControllable)
            {
                highlightedControllable = mouseControllable;
                HighlightControllable(mouseControllable);
            }
            if (Input.GetMouseButtonDown(0)) TakeOverControllable(mouseControllable);
        }
        else
        {
            DeHighlightControllable(highlightedControllable);
            highlightedControllable = null;
        }
        
        
        if (mouseControllable == null && highlightedControllable != null
            || mouseControllable != null && highlightedControllable != null && highlightedControllable != mouseControllable)
        {
            
            if (mouseControllable != null && highlightedControllable != null &&
                highlightedControllable != mouseControllable)
            {
                highlightedControllable = mouseControllable;
                HighlightControllable(mouseControllable);
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

        if (horizontalValue < 0) controllable.OnLeftAction();
        if (horizontalValue > 0) controllable.OnRightAction();
        if (Input.GetButton("Jump")) controllable.OnJumpAction();
        if (Input.GetButton("Special")) controllable.OnSpecialAction();
    }

    protected virtual void TakeOverControllable(BaseControllable other)
    {
        // Hide player if currently under control
        if (controllable.gameObject == Player)
        {
            Player.SetActive(false);
        }
        
        // Set new controller
        controllable = other;
        cameraController.SetTarget(other.gameObject.transform);

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
        controllable = Player.GetComponent<BaseControllable>();
        cameraController.SetTarget(Player.transform);
        
        canSurrenderControl = false;
    }

    private void HighlightControllable(Component ctrl)
    {
        ctrl.GetComponent<SpriteRenderer>().material = HighlightMaterial;

        Debug.DrawLine(controllable.gameObject.transform.position, ctrl.transform.position, Color.green);
        Debug.Log($"Highlighting object {ctrl.gameObject.name}");
    }

    private void DeHighlightControllable(Component ctrl)
    {
        if (ctrl == null) return;
        ctrl.GetComponent<SpriteRenderer>().material = DefaultObjectMaterial;
        Debug.Log($"De-Highlighting object {ctrl.gameObject.name}");
    }

    protected virtual void OnDrawGizmos()
    {
        //var position = controllable?.gameObject.transform.position ?? Player.transform.position;
        //Gizmos.DrawWireSphere(position, ControlReach);
    }

    private BaseControllable TryGetControllableOnMousePosition()
    {
        bool InReach(BaseControllable other)
        {
            var playerPosition = controllable.gameObject.transform.position;
            var distance = Vector3.Distance(other.transform.position, playerPosition);
            return distance < ControlReach;
        }

        // Find Controllables in reach
        var controllables = FindObjectsOfType<BaseControllable>()
            .Where(c => c != controllable)
            .Where(InReach);

        // Check mouse is over
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return controllables
            .Where(c => c.gameObject.GetComponent<Collider2D>() != null)
            .FirstOrDefault(c => c.gameObject.GetComponent<Collider2D>().OverlapPoint(mousePos));
    }
}
