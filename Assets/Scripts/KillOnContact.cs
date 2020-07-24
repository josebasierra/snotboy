using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnContact : MonoBehaviour
{
    [SerializeField] bool useTrigger = true;
    [SerializeField] bool useCollider = true;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!useTrigger) return;

        Kill(collision.gameObject);
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!useCollider) return;

        Kill(collision.gameObject);
    }


    void Kill(GameObject objectToKill)
    {
        var deathComponent = objectToKill.GetComponent<Death>();
        if (deathComponent != null)
        {
            var snotController = objectToKill.GetComponent<SnotController>();
            if (snotController == null || !snotController.IsInsideControllableCollider())
            {
                deathComponent.Die();
            }
        }



    }
}
