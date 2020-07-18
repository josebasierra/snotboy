using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WinMenu : MonoBehaviour
{
    [SerializeField] Button retryButton;
    [SerializeField] Button levelSelectionButton;
    [SerializeField] Button exitButton;
    [SerializeField] GameObject canvasObject;

    void Start()
    {
        canvasObject.SetActive(false);
        retryButton?.onClick.AddListener(GameManager.Instance().ReloadScene);
        levelSelectionButton?.onClick.AddListener(GameManager.Instance().LoadLevelSelection);
        exitButton?.onClick.AddListener(GameManager.Instance().QuitGame);
    }

    public void Show()
    {
        canvasObject.SetActive(true);
    }

    public void Hide()
    {
        canvasObject.SetActive(false);
    }

}

