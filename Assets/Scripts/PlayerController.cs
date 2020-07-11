using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float ControlReach = 1.5f;
    BControllable controllable;

    void Start()
    {
        controllable = GetComponent<BControllable>();
    }


    //TODO: Get input in Update method and then process it in FixedUpdate
    void FixedUpdate()
    {
        var horizontalValue = Input.GetAxis("Horizontal");

        if (horizontalValue < 0) controllable.OnLeftKey();
        if (horizontalValue > 0) controllable.OnRightKey();
        if (Input.GetButton("Jump")) controllable.OnJumpKey();
        if (Input.GetButton("Special")) controllable.OnSpecialKey();
        
        HighlightControllableUnderMouse();

        //GetClose...
        // if hit.collider != null && distance ok && click
        //remove and add playerController...
    }

    private void HighlightControllableUnderMouse()
    {
        var mouseControllable = TryGetControllableOnMousePosition();
        if (mouseControllable == null) return;
        
        Debug.DrawLine(this.transform.position, mouseControllable.transform.position, Color.green);

        Debug.Log($"Highlighting object {mouseControllable.gameObject.name}");
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, ControlReach);
    }

    private BControllable TryGetControllableOnMousePosition()
    {
        bool InReach(BControllable other)
        {
            var distance = (other.transform.position - transform.position).sqrMagnitude;
            return distance < ControlReach;
        }
        
        // Find Controllables in reach
        var controllables = FindObjectsOfType<BControllable>()
            .Where(c => c != this)
            .Where(InReach);

        // Check mouse is over
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return controllables
            .Where(c => c.gameObject.GetComponent<Collider2D>())
            .FirstOrDefault(c => c.gameObject.GetComponent<Collider2D>().OverlapPoint(mousePos));
    }
}
