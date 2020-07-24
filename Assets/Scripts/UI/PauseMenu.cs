using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PauseMenu : MonoBehaviour
{
    GameObject canvasObject;

    bool canPause = true;
    bool isPaused = false;


    void Start()
    {
        canvasObject = this.transform.GetChild(0).gameObject;
        canvasObject.SetActive(false);

        GameManager.Instance().OnWin += OnWin;
    }


    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (!isPaused && canPause)
            {
                isPaused = true;
                canvasObject.SetActive(true);
                GameManager.Instance().Pause();
            }
            else if (isPaused)
            {
                isPaused = false;
                canvasObject.SetActive(false);
                GameManager.Instance().Continue();
            }
        }
    }


    void OnWin()
    {
        canPause = false;
    }


    void OnDestroy()
    {
        GameManager.Instance().OnWin -= OnWin;
    }
}
