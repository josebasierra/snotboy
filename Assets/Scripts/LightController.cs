using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] GameObject light;

    Transform source;


    public void SetSource(Transform source)
    {
        this.source = source;
    }


    void Update()
    {
        light.transform.position = source.position;
        var targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = 0;
        light.transform.up = targetPosition - source.position;
    }
}
