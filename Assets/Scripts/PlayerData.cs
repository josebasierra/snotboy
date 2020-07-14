using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//SINGLETON CLASS
public class PlayerData : MonoBehaviour
{
    [SerializeField] GameObject playerBody;
    [SerializeField] float controlReach;
    [SerializeField] Material highlightMaterial;

    private static PlayerData _instance;


    public static PlayerData Instance()
    { 
        return _instance; 
    }

    public float GetControlReach()
    {
        return controlReach;
    }

    public GameObject GetPlayerBody()
    {
        return playerBody;
    }

    public Material GetHighlightMaterial()
    {
        return highlightMaterial;
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
            if (playerBody == null)
            {
                playerBody = GameObject.FindGameObjectWithTag("Player");
            }
        }
    }
}
