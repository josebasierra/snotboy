using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Controllable : MonoBehaviour
{
    [SerializeField] float unavailableTime = 1f;

    private Highlighter highlighter;
    private bool isAvailable = true;  //object become available to control again after X seconds of being released
    private bool isBeingControlled = false;


    private void Start()
    {
        highlighter = gameObject.AddComponent<Highlighter>();
        this.gameObject.layer = 10; //set object layer to controllable
    }


    public void SetIsBeingControlled(bool value)
    {
        isBeingControlled = value;
        if (!value)
        {
            isAvailable = false;
            Invoke("MakeAvailable", 1f);
        }
    }


    public bool IsAvailable()
    {
        return isAvailable;
    }


    private void MakeAvailable()
    {
        isAvailable = true;
    }


    public bool IsBeignControlled()
    {
        return isBeingControlled;
    }
    

    private void OnMouseEnter()
    {
        highlighter?.HighlightOn();
    }


    private void OnMouseExit()
    {
        highlighter?.HighlightOff();
    }
}
