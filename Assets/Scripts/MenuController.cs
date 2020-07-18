using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    private static MenuController instance;
    private GameObject Menu;
    
    // Start is called before the first frame update
    void Start()
    {
        Menu = findMenuGameObject();
        
        HideMenu();
        Menu.transform.Find("ContinueButton").GetComponent<Button>()?.onClick.AddListener(Continue);
        Menu.transform.Find("RetryButton").GetComponent<Button>()?.onClick.AddListener(Retry);
        Menu.transform.Find("ExitButton").GetComponent<Button>()?.onClick.AddListener(Exit);
        instance = this;
    }

    private GameObject findMenuGameObject()
    {
        var canvasTransform = transform.Find("Canvas");
        var menu = canvasTransform.Find("Menu");
        return menu == null ? null : menu.gameObject;
    }
    
    public MenuController GetInstance()
    {
        return instance;
    }

    private void Continue()
    {
        HideMenu();
    }
    
    private void Retry()
    {
        GameManager.Instance().ReloadScene();
    }

    private void Exit()
    {
        GameManager.Instance().QuitGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (MenuActive())
                HideMenu();
            else
                ShowMenu();
        }
    }

    public bool MenuActive()
    {
        return Menu.activeSelf;
    }

    public void ShowMenu()
    {
        Menu.SetActive(true);
    }

    public void HideMenu()
    {
        Menu.SetActive(false);
    }
}
