using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class SwitchInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] GameObject switchableObject;
        [SerializeField] float switchCooldown = 1f;

        private bool isOnCooldown = false;
        private bool state = false;

        [Header("Sounds")]
        [SerializeField] AudioClip clickSound;
        AudioSource audioSource;

        public bool State => state;
        
        void Start()
        {
            switchableObject.SetActive(state);
            audioSource = GetComponent<AudioSource>();
        }


        public void Interact()
        {
            if (!isOnCooldown)
            {
                //delay switch to give some time to the player
                Invoke(nameof(Switch), switchCooldown);

                isOnCooldown = true;
                Invoke(nameof(EnableSwitch), switchCooldown);

                AudioManager.PlayShot(audioSource, clickSound);
            }
        }

        void Switch()
        {
            state = !state;
            switchableObject.SetActive(state);
        }


        void EnableSwitch()
        {
            isOnCooldown = false;
        }
        
    }
}
