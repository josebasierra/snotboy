using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShooterControllable : StaticControllable
{
    [SerializeField] GameObject bullet;

    float shootCooldown;
    bool canShoot;



    //TODO: ...
    void Update()
    {
        Vector2 aimTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 currentPosition = transform.position;
    }

    public override void OnSpecialAction()
    {
        
    }
}
