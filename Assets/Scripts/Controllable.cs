using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Controllable : MonoBehaviour
{
    [SerializeField] float unavailableTime = 1f;

    Highlighter highlighter;

    //object become available to control again after X seconds of being released
    bool isAvailable = true;
    bool isBeingControlled = false;


    private void Start()
    {
        highlighter = gameObject.AddComponent<Highlighter>();
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


    private void OnMouseEnter()
    {
        highlighter?.HighlightOn();
    }


    private void OnMouseExit()
    {
        highlighter?.HighlightOff();
    }
}
