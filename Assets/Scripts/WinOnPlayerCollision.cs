using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinOnPlayerCollision : MonoBehaviour
{
    WinMenu winMenu;

    void Start()
    {
        winMenu = FindObjectOfType<WinMenu>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            winMenu.Show();
        }
    }
}
