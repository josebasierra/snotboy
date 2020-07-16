using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Highlighter : MonoBehaviour
{
    [SerializeField] Material highlightMaterial;

    SpriteRenderer spriteRenderer;
    Material originalMaterial;

    bool isHighlighted = false;


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }


    public void HighlightOn()
    {
        if (isHighlighted) return;

        if (highlightMaterial != null)
        {
            spriteRenderer.material = highlightMaterial;
        }
        else
        {
            spriteRenderer.material = GameManager.Instance().GetHighlightMaterial();
        }

        isHighlighted = true;
    }


    public void HighlightOff()
    {
        if (!isHighlighted) return;

        spriteRenderer.material = originalMaterial;
        isHighlighted = false;
    }
}
