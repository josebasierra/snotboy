using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class ShootInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] float shootForce = 30f;
        [SerializeField] float shootDelay = 1f;
        [SerializeField] float shootCooldown = 1f;

        [SerializeField] bool needsAmmunition = false;
        [SerializeField] protected int ammunition = 0;  //does not require ammunition if value = -1 
        [SerializeField] int maxAmmunition = 2;

        [SerializeField] GameObject objectToShoot;
        [SerializeField] Transform releasePoint;

        bool isShotOnCooldown = false;

        [Header("Sounds")]
        [SerializeField] AudioClip clickSound;
        [SerializeField] AudioClip shootSound;
        AudioSource audioSource;


        protected void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }


        public void Interact()
        {
            if (isShotOnCooldown) return;
            if (needsAmmunition && ammunition <= 0) return;

            Invoke(nameof(Shoot), shootDelay);

            ammunition -= 1;
            isShotOnCooldown = true;
            Invoke(nameof(EnableShoot), shootCooldown);

            AudioManager.PlayShot(audioSource, clickSound);
        }


        protected virtual void Shoot()
        {
            var firedObject = Instantiate(objectToShoot);
            firedObject.name = objectToShoot.name;
            firedObject.transform.position = releasePoint.position;

            var shootDirection = (releasePoint.position - transform.position).normalized;
            firedObject.GetComponent<Rigidbody2D>().AddForce(shootDirection * shootForce, ForceMode2D.Impulse);

            AudioManager.PlayShot(audioSource, shootSound);
        }


        protected virtual void Reload()
        {
            ammunition++;
        }


        void EnableShoot()
        {
            isShotOnCooldown = false;
        }


        void OnCollisionStay2D(Collision2D collision)
        {
            if (needsAmmunition && ammunition < maxAmmunition && !objectToShoot.CompareTag("Untagged") && objectToShoot.CompareTag(collision.gameObject.tag))
            {
                Destroy(collision.gameObject);
                Reload();
            }
        }
    }

    
}
