using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTextSortingLayer : MonoBehaviour
{
    public int sortingOrder = 10;

    void Start()
    {
        var mRenderer = GetComponent<MeshRenderer>();
        mRenderer.sortingOrder = sortingOrder;
    }
}
