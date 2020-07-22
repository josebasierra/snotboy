using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatMessage : MonoBehaviour
{
    Transform playerTransform;

    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        GameManager.Instance().OnDefeat += OnDefeat;
    }

    void FixedUpdate()
    {
        transform.position = playerTransform.position;
    }

    void OnDefeat()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
