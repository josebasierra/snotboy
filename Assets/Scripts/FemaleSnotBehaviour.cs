using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FemaleSnotBehaviour : MonoBehaviour
{
    [SerializeField] float impactForceToKill = 1f;
    [SerializeField] GameObject deathEffect;
    [SerializeField] AudioClip deathSound;
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
            var effect = Instantiate(deathEffect);
            effect.transform.position = this.transform.position;
            Destroy(this.gameObject);

            GameManager.Instance().Defeat();
            AudioManager.Instance().PlayShot(deathSound);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance().Win();
            AudioManager.Instance().PlayShot(loveSound);
        }
    }


}
