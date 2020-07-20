using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WinMenu : MonoBehaviour
{
    GameObject canvasObject;


    void Start()
    {
        canvasObject = this.transform.GetChild(0).gameObject;
        canvasObject.SetActive(false);
        GameManager.Instance().OnWin += OnWin;
    }


    private void OnDestroy()
    {
        GameManager.Instance().OnWin -= OnWin;
    }


    void OnWin()
    {
        Debug.Log("ok");
        canvasObject.SetActive(true);
    }
}

