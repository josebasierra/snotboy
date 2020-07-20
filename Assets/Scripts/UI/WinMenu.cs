using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WinMenu : MonoBehaviour
{
    GameObject canvasObject;

    void Start()
    {
        canvasObject.SetActive(false);
    }

    private void OnEnable()
    {
        //subscribe to GameManager win event ...
    }

    private void OnDisable()
    {
        
    }

    void OnWin()
    {

    }
}

