﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float smooth = 0.01f;
    Transform target;


    public void SetTarget(Transform target)
    {
        this.target = target;
    }


    void FixedUpdate()
    {
        if (target == null) return;

        var currentPosition = transform.position;
        var targetPosition = target.position;

        var newPosition = Vector3.Lerp(currentPosition, targetPosition, smooth);
        transform.position = new Vector3(newPosition.x, newPosition.y, currentPosition.z);
    }
}
