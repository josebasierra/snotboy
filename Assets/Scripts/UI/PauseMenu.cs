using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PauseMenu : MonoBehaviour
{
    GameObject canvasObject;


    void Start()
    {
        canvasObject = this.transform.GetChild(0).gameObject;
        canvasObject.SetActive(false);
    }


    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            canvasObject.SetActive(!canvasObject.activeSelf);
        }
    }
}
