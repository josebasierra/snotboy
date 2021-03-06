﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

//SINGLETON CLASS	
public class GameManager : MonoBehaviour
{
    [SerializeField] Material defaultHighlightMaterial;
    [SerializeField] Material defaultUnderControlMaterial;

    public event Action OnWin;
    static GameManager instance;

    bool isPaused = false;
    float currentLevelTime = 0f;


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


    void Update()
    {
        if (!isPaused)
        {
            currentLevelTime += Time.deltaTime;
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

    public float GetCurrentLevelTime()
    {
        return currentLevelTime;
    }

    public void Win()
    {
        OnWin?.Invoke();
        Debug.Log("Level completed in " + currentLevelTime.ToString());
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;
    }

    public void Continue()
    {
        isPaused = false;
        Time.timeScale = 1;
    }


    // Scene management

    public void LoadScene(string sceneName)
    {
        Continue();
        currentLevelTime = 0;
        SceneManager.LoadScene(sceneName);
    }

    public void ReloadScene()
    {
        LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();   
    }
}
