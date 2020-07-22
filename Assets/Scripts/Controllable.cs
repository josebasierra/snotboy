using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;


public class Controllable : MonoBehaviour
{
    [SerializeField] float unavailableTime = 1f;

    private Highlighter highlighter;
    private UnderControlHighlighter underControlHighlighter;
    private bool isAvailable = true;  //object become available to control again after X seconds of being released
    private bool isBeingControlled = false;
    private bool mouseOver = false;

    private void Start()
    {
        highlighter = gameObject.AddComponent<Highlighter>();
        underControlHighlighter = gameObject.AddComponent<UnderControlHighlighter>();
        gameObject.layer = 10; //set object layer to controllable
    }


    public void SetIsBeingControlled(bool value)
    {
        isBeingControlled = value;
        if (!value)
        {
            isAvailable = false;
            Invoke("MakeAvailable", 1f);
        }

        if (value)
            underControlHighlighter.HighlightOn();
        else
            underControlHighlighter.HighlightOff();;
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
        if (isBeingControlled) return;
        
        mouseOver = true;
        highlighter?.HighlightOn();
    }


    private void OnMouseExit()
    {
        if (isBeingControlled) return;
        
        mouseOver = false;
        highlighter?.HighlightOff();
    }
}
