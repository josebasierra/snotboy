using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SignController : MonoBehaviour
{
    private Collider2D myCollider;
    private GameObject myTextObject;
    private TextMeshPro text;

    private void Start()
    {
        myTextObject = transform.Find("SignText").gameObject;
        myCollider = GetComponent<Collider2D>();
        text = myTextObject.GetComponent<TextMeshPro>();
        
        HideText();
    }


    private void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("object entered");
        if (other.gameObject.CompareTag("Player"))
        {
            ShowText();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("object exited");
        if (other.gameObject.CompareTag("Player"))
        {
            HideText();
        }
    }

    public void HideText()
    {
        text.enabled = false;
    }

    public void ShowText()
    {
        text.enabled = true;
    }
}
