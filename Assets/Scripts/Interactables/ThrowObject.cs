using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject objectToBeThrown;
    [SerializeField] Transform aimTarget;
    [SerializeField] float throwForce;

    public void Interact()
    {
        
    }
}
