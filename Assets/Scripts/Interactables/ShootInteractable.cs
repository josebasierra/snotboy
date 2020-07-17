using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class ShootInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] float shootForce = 30f;
        [SerializeField] float shootCooldown = 1f;
        [SerializeField] int ammunition = -1;  //does not require ammunition if value = -1 

        [SerializeField] GameObject objectToShoot;
        [SerializeField] Transform releasePoint;

        bool isShotOnCooldown = false;


        public void Interact()
        {
            if (isShotOnCooldown) return;
            if (ammunition != -1 && ammunition <= 0) return;

            var firedObject = Instantiate(objectToShoot);
            firedObject.transform.position = releasePoint.position;

            var shootDirection = (releasePoint.position - transform.position).normalized;
            firedObject.GetComponent<Rigidbody2D>().AddForce(shootDirection * shootForce, ForceMode2D.Impulse);

            if (ammunition != -1) ammunition -= 1;

            isShotOnCooldown = true;
            Invoke("EnableShoot", shootCooldown);

            Debug.Log("Shot fired");
        }


        void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject == objectToShoot)
            {
                Destroy(collision.gameObject);
                ammunition++;
            }
        }


        void EnableShoot()
        {
            isShotOnCooldown = false;
        }
    }

    
}
