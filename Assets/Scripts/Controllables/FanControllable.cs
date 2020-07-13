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

            // HACK: (if wind area effector is activated having an object inside, its not affected until moving it, reseting collider parent, etc...)
            myCollider.enabled = false;
            myCollider.enabled = true;

            canSwitch = false;
            Invoke("EnableSwitch", switchCooldown);
        }
    }


    private void EnableSwitch()
    {
        canSwitch = true;
    }
}
