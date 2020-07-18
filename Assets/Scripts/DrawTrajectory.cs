using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTrajectory : MonoBehaviour
{
    public float force = 1f;
    public float mass = 1f;
    public float maxTime = 2f;
    public int pointsCount = 20;


    void Update()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        var direction = (mousePosition - transform.position).normalized;
        var velocity = (direction * force)/mass;
        var acceleration = Physics2D.gravity;

        var trajectoryPoints = GetTrajectoryPoints(transform.position, velocity, acceleration, maxTime, pointsCount);
        Draw(trajectoryPoints);
    }


    Vector2[] GetTrajectoryPoints(Vector2 position, Vector2 velocity, Vector2 acceleration, float maxTime, int pointsCount)
    {
        var points = new Vector2[pointsCount]; 
        for (int i = 0; i < pointsCount; i++)
        {
            float time = i * (maxTime / pointsCount);
            points[i] = PositionAtTime(position, velocity, acceleration, time);
        }
        return points;
    }


    Vector2 PositionAtTime(Vector2 position, Vector2 v, Vector2 a, float t)
    {
        return position + Displacement(v, a, t);
    }


    Vector2 Displacement(Vector2 v, Vector2 a, float t)
    {
        return v * t + 0.5f * a * t*t;
    }


    void Draw(Vector2[] points)
    {
        for(int i = 0; i < points.Length - 1; i++)
        {
            Debug.DrawLine(points[i], points[i + 1]);
        }
    }

}
