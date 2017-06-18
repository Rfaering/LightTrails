using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticlePlex : MonoBehaviour
{
    public float MaxDistance = 1.0f;

    public int MaxConnections = 5;
    public int MaxLineRenderers = 100;

    new ParticleSystem particleSystem;

    ParticleSystem.Particle[] particles;
    ParticleSystem.MainModule particleSystemMainModule;

    public LineRenderer lineRendererTemplate;
    public List<LineRenderer> lineRendederers = new List<LineRenderer>();

    public Transform _tranform;

    // Use this for initialization
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particleSystemMainModule = particleSystem.main;
    }

    private void LateUpdate()
    {
        if (lineRendererTemplate == null)
        {
            return;
        }

        int maxParticles = particleSystemMainModule.maxParticles;

        if (particles == null || particles.Length < maxParticles)
        {
            particles = new ParticleSystem.Particle[maxParticles];
        }

        particleSystem.GetParticles(particles);
        int particleCount = particleSystem.particleCount;

        float maxDistnceSqr = MaxDistance * MaxDistance;

        int lrIndex = 0;
        int lineRendererCount = lineRendederers.Count;

        switch (particleSystemMainModule.simulationSpace)
        {
            case ParticleSystemSimulationSpace.Local:
                _tranform = transform;
                lineRendererTemplate.useWorldSpace = false;
                break;
            case ParticleSystemSimulationSpace.World:
                _tranform = transform;
                lineRendererTemplate.useWorldSpace = true;
                break;
            case ParticleSystemSimulationSpace.Custom:
                _tranform = particleSystemMainModule.customSimulationSpace;
                lineRendererTemplate.useWorldSpace = false;
                break;
            default:
                break;
        }

        for (int i = 0; i < particleCount; i++)
        {
            if (lrIndex == MaxLineRenderers)
            {
                break;
            }

            Vector3 p1Position = particles[i].position;

            var currentConnections = 0;

            for (int j = i + 1; j < particleCount; j++)
            {
                Vector3 p2Position = particles[j].position;
                float distanceSqr = Vector3.SqrMagnitude(p1Position - p2Position);

                if (distanceSqr <= maxDistnceSqr)
                {
                    LineRenderer lineRenderer;
                    if (lrIndex == lineRendererCount)
                    {
                        lineRenderer = Instantiate(lineRendererTemplate, transform, false);
                        lineRendederers.Add(lineRenderer);
                        lineRendererCount++;
                    }

                    lineRenderer = lineRendederers[lrIndex];
                    lineRenderer.enabled = true;

                    lineRendederers[lrIndex].startColor = particles[i].GetCurrentColor(particleSystem);
                    lineRendederers[lrIndex].endColor = particles[j].GetCurrentColor(particleSystem);

                    lineRendederers[lrIndex].SetPosition(0, p1Position);
                    lineRendederers[lrIndex].SetPosition(1, p2Position);

                    currentConnections++;
                    lrIndex++;
                    if (currentConnections == MaxConnections || lrIndex == MaxLineRenderers)
                    {
                        break;
                    }
                }
            }
        }

        for (int i = lrIndex; i < lineRendederers.Count; i++)
        {
            lineRendederers[i].enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
