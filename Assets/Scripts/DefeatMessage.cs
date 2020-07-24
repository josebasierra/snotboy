using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatMessage : MonoBehaviour
{
    Transform playerTransform;

    GameObject snotPrincess;
    Death snotPrincessDeath;

    GameObject defeatMessage;


    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        snotPrincess = GameObject.FindGameObjectWithTag("Princess");
        defeatMessage = transform.GetChild(0).gameObject;

        defeatMessage.SetActive(false);

        if (snotPrincess != null) snotPrincessDeath = snotPrincess.GetComponent<Death>();

        if (snotPrincessDeath != null)
        {
            snotPrincessDeath.OnDeath += OnDeath;
        }
    }


    void FixedUpdate()
    {
        if (playerTransform != null)
            transform.position = playerTransform.position;
    }


    void OnDeath()
    {
        defeatMessage.SetActive(true);
    }


    private void OnDestroy()
    {
        if (snotPrincess != null && snotPrincessDeath != null)
        {
            snotPrincessDeath.OnDeath -= OnDeath;
        }
    }
}
