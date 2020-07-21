using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnTrigger : MonoBehaviour
{
    [SerializeField] AudioClip sound;
    AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = sound;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (source != null && sound != null && !source.isPlaying)
        {
            if (collision.CompareTag("Player")) source.Play();
        }
    }
}
