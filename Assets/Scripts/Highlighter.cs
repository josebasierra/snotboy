using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Highlighter : MonoBehaviour
{
    [SerializeField] Material highlightMaterial;

    SpriteRenderer spriteRenderer;
    Material originalMaterial;

    protected virtual Material DefaultHighlightMaterial => GameManager.Instance().GetHighlightMaterial();

    bool isHighlighted = false;
    
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }
    
    public void SetHighlightMaterial(Material material)
    {
        highlightMaterial = material;
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
            spriteRenderer.material = DefaultHighlightMaterial;
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
