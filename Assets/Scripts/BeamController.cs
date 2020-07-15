using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamController : MonoBehaviour
{
    [SerializeField] GameObject beam;

    float range = 2f;
    Transform source;

    ParticleSystem beamParticleSystem;
    ParticleSystem.Particle[] beamParticlesBuffer;


    public void SetSource(Transform newSource)
    {
        if (source != null)
        {
            // set collision layer to Default
            source.gameObject.layer = 0;
        }
        source = newSource;

        // set collision layer to IgnoreParticles
        source.gameObject.layer = 8;
    }


    public void SetRange(float value)
    {
        range = value;
    } 


    void Start()
    {
        beamParticleSystem = beam.GetComponent<ParticleSystem>();
        beamParticlesBuffer = new ParticleSystem.Particle[beamParticleSystem.maxParticles];
    }


    void Update() 
    {
        if (beam == null || source == null) return;

        beam.transform.position = source.position;
        var targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = 0;
        beam.transform.right = targetPosition - source.position;

        KillParticlesOutsideRange();
    }


    void KillParticlesOutsideRange()
    {
        int numParticlesAlive = beamParticleSystem.GetParticles(beamParticlesBuffer);

        for (int i = 0; i < numParticlesAlive; i++)
        {
            var particle = beamParticlesBuffer[i];

            // particle in local positions -> distance to source = position.magnitude
            if (particle.position.magnitude > range)
            {
                particle.remainingLifetime = 0;
            }
            beamParticlesBuffer[i] = particle;
        }

        beamParticleSystem.SetParticles(beamParticlesBuffer, numParticlesAlive);
    }
}
