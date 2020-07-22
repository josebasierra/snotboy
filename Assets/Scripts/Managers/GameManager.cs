using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

//SINGLETON CLASS	
public class GameManager : MonoBehaviour
{
    [SerializeField] Material defaultHighlightMaterial;
    [SerializeField] Material defaultUnderControlMaterial;

    public event Action OnWin, OnDefeat;
    static GameManager instance;

    public static GameManager Instance()
    {
        return instance;
    }


    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }


    public Material GetHighlightMaterial()
    {
        return defaultHighlightMaterial;
    }

    public Material GetUnderControlMaterial()
    {
        return defaultUnderControlMaterial;
    }

    public void Defeat()
    {
        Debug.Log("Defeat notified to Game Manager");
        OnDefeat?.Invoke();
    }

    public void Win()
    {
        Debug.Log("Win notified to Game Manager");
        OnWin?.Invoke();
    }


    //scene management

    public void LoadScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }


    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void QuitGame()
    {
        Application.Quit();   
    }
}
