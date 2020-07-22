using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToasterInteractable : ShootInteractable, IInteractable
{
    [Header("Sprites")]
    [SerializeField] Sprite empty;
    [SerializeField] Sprite oneToast;
    [SerializeField] Sprite twoToast;

    SpriteRenderer spriteRenderer;
    int oldAmmunition;


    void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        oldAmmunition = ammunition;
        UpdateSprite();
    }


    protected override void Shoot()
    {
        base.Shoot();
        UpdateSprite();
    }


    protected override void Reload()
    {
        base.Reload();
        UpdateSprite();
    }


    void UpdateSprite()
    {
        if (ammunition <= 0) spriteRenderer.sprite = empty;
        else if (ammunition == 1) spriteRenderer.sprite = oneToast;
        else spriteRenderer.sprite = twoToast;
    }
}
