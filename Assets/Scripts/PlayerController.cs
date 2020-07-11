using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float ControlReach = 1.5f;
    BControllable controllable;
    CameraController cameraController;


    void Start()
    {
        controllable = GetComponent<BControllable>();
        cameraController = Camera.main.GetComponent<CameraController>();
        cameraController.SetTarget(this.transform);
    }


    //TODO: Get input in Update method and then process it in FixedUpdate
    void FixedUpdate()
    {
        var horizontalValue = Input.GetAxis("Horizontal");
        if (horizontalValue < 0) controllable.OnLeftKey();
        if (horizontalValue > 0) controllable.OnRightKey();
        if (Input.GetButton("Jump")) controllable.OnJumpKey();
        if (Input.GetButton("Special")) controllable.OnSpecialKey();


        var closeControllables = GetCloseControllables();
        foreach (var closeControllable in closeControllables)
        {
            Debug.DrawLine(this.transform.position, closeControllable.transform.position, Color.green);
        }

        

        //GetClose...
        // if hit.collider != null && distance ok && click
        //remove and add playerController...

    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, ControlReach);
    }

    // hit = raycast() 
    // collider.GetComponent<BControllable>

    private IEnumerable<Controllable> GetCloseControllables()
    {
        bool InReach(Controllable other)
        {
            var distance = (other.transform.position - transform.position).sqrMagnitude;
            return distance < ControlReach;
        }
        //raycast
        var controllables = FindObjectsOfType<Controllable>()
            .Where(c => c != this)
            .Where(InReach);

        return controllables;
    }
}
