using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip buttonSound;
    AudioSource audioSource;
    Transform mainCamera;

    static AudioManager instance;
    public static AudioManager Instance()
    {
        return instance;
    }


    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(this.gameObject);
        }
    }


    void FixedUpdate()
    {
        transform.position = mainCamera.position;
    }


    public void PlayMusic(AudioClip music)
    {
        if (music == audioSource.clip) return;

        audioSource.clip = music;
        audioSource.Play();
    }


    public void PlayButtonSound()
    {
        audioSource.PlayOneShot(buttonSound);
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        mainCamera = Camera.main.transform;
    }


    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
