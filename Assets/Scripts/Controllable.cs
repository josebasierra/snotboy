using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Controllable : MonoBehaviour
{
    bool isBeingControlled = false;
    bool isAvailable = true;

    public void SetIsBeingControlled(bool value)
    {
        isBeingControlled = value;
        if (!value)
        {
            isAvailable = false;
            Invoke("MakeAvailable", 0.5f);
        }
    }

    public bool IsAvailable()
    {
        return isAvailable;
    }

    void MakeAvailable()
    {
        isAvailable = true;
    }

    //TODO:
    // if (undercontrol)/(has playerController component):
    // visual effect

}
