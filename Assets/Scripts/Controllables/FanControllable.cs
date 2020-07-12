using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanControllable : StaticControllable
{
    [SerializeField] GameObject windObject;
    [SerializeField] float switchCooldown = 0.3f;

    bool canSwitch = true;
    bool state = false;


    private void Start()
    {
        base.Start();
        windObject.SetActive(state);    
    }


    public override void OnSpecialKey()
    {
        if (canSwitch)
        {
            state = !state;
            windObject.SetActive(state);

            canSwitch = false;
            Invoke("EnableSwitch", switchCooldown);
        }
    }


    private void EnableSwitch()
    {
        canSwitch = true;
    }
}
