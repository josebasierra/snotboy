using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using JetBrains.Annotations;
using UnityEngine;

public class Controllable : MonoBehaviour, IControllable
{
    public bool ControlledByPlayer = false;
    public float ControlReach = 1.5f; 
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!ControlledByPlayer) return;
        
        var closeControllables = GetCloseControllables();
        foreach (var closeControllable in closeControllables)
        {
            Debug.DrawLine(this.transform.position, closeControllable.transform.position, Color.green);
        }
    }

    private void OnDrawGizmos()
    {
        if (!ControlledByPlayer) return;
        Gizmos.DrawWireSphere(transform.position, ControlReach);
    }

    public void TakeControl([NotNull] IControllable other)
    {
        Disable();
        other.Enable();
    }

    public void SurrenderControl()
    {
        Disable();
        
        // Enable player
        // Move player to current position
        // Take control over Player Controllable
    }

    public void Enable()
    {
        ControlledByPlayer = true;
    }

    public void Disable()
    {
        ControlledByPlayer = false;
    }

    private IEnumerable<Controllable> GetCloseControllables()
    {
        bool InReach(Controllable other)
        {
            var distance = (other.transform.position - transform.position).sqrMagnitude;
            return distance < ControlReach;
        }

        var controllables = FindObjectsOfType<Controllable>()
            .Where(c => c != this)
            .Where(InReach);

        return controllables;
    }
}
