using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Death : MonoBehaviour
{
    public event Action OnDeath;

    [SerializeField] GameObject deathEffect;
    [SerializeField] AudioClip deathSound;


    public void Die()
    {
        var effect = Instantiate(deathEffect);
        effect.transform.position = this.transform.position;

        OnDeath?.Invoke();
        AudioManager.Instance().PlayShot(deathSound);

        Destroy(this.gameObject);
    }
}
