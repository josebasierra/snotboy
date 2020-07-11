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
        bool InReach(Component other)
        {
            var distance = (other.transform.position - transform.position).sqrMagnitude;
            return distance < ControlReach;
        }
        
        // Try get controllable object under mouse
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider == null)
        {
            Debug.Log("no hit");
            return null;
        }

        if (hit.collider.gameObject.GetComponent<BControllable>() == null)
        {
            Debug.Log("Object has no controllable");
            Debug.Log(hit.collider.gameObject.name);
            Debug.Log(hit.collider.gameObject);
            return null;
        }
        
        var controllableUnderMouse = hit.collider.gameObject.GetComponent<BControllable>();

        if (controllableUnderMouse == controllable) return null;

        return InReach(controllableUnderMouse)
            ? controllableUnderMouse
            : null;


    }
}
