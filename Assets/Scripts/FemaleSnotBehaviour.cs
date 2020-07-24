using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FemaleSnotBehaviour : MonoBehaviour
{
    [SerializeField] float impactForceToKill = 1f;
    [SerializeField] AudioClip loveSound;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        var impactForce = 0f;
        foreach (var contact in collision.contacts)
        {
            impactForce += contact.normalImpulse;
        }
        impactForce = impactForce / Time.fixedDeltaTime;

        Debug.Log(impactForce);
        if (impactForce >= impactForceToKill)
        {
            GetComponent<Death>()?.Die();
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance().Win();
            AudioManager.Instance().PlayShot(loveSound);
        }
    }


}
