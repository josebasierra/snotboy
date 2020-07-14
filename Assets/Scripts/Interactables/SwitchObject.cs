using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchObject : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject switchableObject;
    [SerializeField] float switchCooldown;

    bool isOnCooldown = false;
    bool state = false;


    void Start()
    {
        switchableObject.SetActive(state);
    }


    public void Interact()
    {
        if (!isOnCooldown)
        {
            state = !state;
            switchableObject.SetActive(state);

            isOnCooldown = true;
            Invoke("EnableSwitch", switchCooldown);
        }
    }


    void EnableSwitch()
    {
        isOnCooldown = false;
    }
}
