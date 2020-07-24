using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class WinMenu : MonoBehaviour
{
    [SerializeField] GameObject canvasObject;
    [SerializeField] GameObject completionTimeObject;


    void Start()
    {
        canvasObject.SetActive(false);
        GameManager.Instance().OnWin += OnWin;
    }


    private void OnDestroy()
    {
        GameManager.Instance().OnWin -= OnWin;
    }


    void OnWin()
    {
        canvasObject.SetActive(true);

        var completionTimeTextMesh = completionTimeObject.GetComponent<TextMeshProUGUI>();
        if (completionTimeTextMesh != null)
        {
            float completionTime = GameManager.Instance().GetCurrentLevelTime();
            int minutes = (int)completionTime / 60;
            int seconds = (int)completionTime % 60;
            completionTimeTextMesh.text = "Completion time: " + minutes.ToString() + "m " + seconds.ToString() + "s";
        }
    }
}

