using Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float controlReach = 4f;

    IControllable controllable;
    CameraController cameraController;


    public void SetControlReach(float controlReach)
    {
        this.controlReach = controlReach;
    } 


    private void Start()
    {
        controllable = GetComponent<IControllable>();
        cameraController = Camera.main.GetComponent<CameraController>();
        cameraController.SetTarget(this.transform);
    }


    private void Update()
    {
        var mouseControllable = TryGetControllableOnMousePosition();
        if (mouseControllable != null)
        {
            HighlightControllable(mouseControllable);

            if (Input.GetMouseButtonDown(0))
            {
                var newPlayerController = mouseControllable.gameObject.AddComponent<PlayerController>();
                newPlayerController.SetControlReach(controlReach);

                cameraController.SetTarget(this.transform);

                Destroy(this); //destroying this instance of PlayerController component  
            }
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


    private void HighlightControllable(MonoBehaviour ctrl)
    {
        Debug.DrawLine(this.transform.position, ctrl.transform.position, Color.green);
        Debug.Log($"Highlighting object {ctrl.gameObject.name}");
    }
    

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, controlReach);
    }


    private MonoBehaviour TryGetControllableOnMousePosition()
    {
        bool InReach(GameObject other)
        {
            //var distance = (other.transform.position - transform.position).sqrMagnitude;
            var distance = Vector3.Distance(other.transform.position, transform.position);
            return distance < controlReach;
        }

        // Find Controllables in reach
        var controllables = FindObjectsOfType<MonoBehaviour>()
            .Where(c => c != this);

        // Check mouse is over
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return controllables
            .Where(c => c.gameObject.GetComponent<Collider2D>())
            .FirstOrDefault(c => c.gameObject.GetComponent<Collider2D>().OverlapPoint(mousePos));
    }
}
