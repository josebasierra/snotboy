using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainView;
    [SerializeField] GameObject controlsView;

    GameObject currentView;


    public void Play()
    {
        GameManager.Instance().LoadLevelSelection();
    }


    public void Exit()
    {
        GameManager.Instance().QuitGame();
    }


    public void ShowMainView()
    {
        ShowView(mainView);
    }


    public void ShowControlsView()
    {
        ShowView(controlsView);
    }


    void Start()
    {
        mainView.SetActive(false);
        controlsView.SetActive(false);
        ShowMainView();
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel") && currentView == controlsView)
        {
            ShowMainView();
        }
    }


    void ShowView(GameObject view)
    {
        if (currentView != null) currentView.SetActive(false);
        currentView = view;
        currentView.SetActive(true);
    }


}
