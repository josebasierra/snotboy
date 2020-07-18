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

    public Material GetHighlightMaterial()
    {
        return defaultHighlightMaterial;
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

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();   
    }
}
