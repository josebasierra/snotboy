using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//SINGLETON CLASS	
public class GameManager : MonoBehaviour
{
    [SerializeField] Material defaultHighlightMaterial;

    static GameManager _instance;
    public static GameManager Instance()
    {
        return _instance;
    }


    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    public Material GetHighlightMaterial()
    {
        return defaultHighlightMaterial;
    }


    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }


    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void LoadLevelSelection()
    {
        SceneManager.LoadScene(1);
    }


    public void QuitGame()
    {
        Application.Quit();   
    }
}
