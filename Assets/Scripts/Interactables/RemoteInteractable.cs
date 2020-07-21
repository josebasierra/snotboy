using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteInteractable : MonoBehaviour, IInteractable
{
    
    [SerializeField] public GameObject ControlledObject;
    
    public void Interact()
    {
        if (ControlledObject == null) return;
        
        var interactable = ControlledObject.GetComponent<IInteractable>();

        interactable?.Interact();
        
    }
}
