using UnityEngine;

public class ScalableParticleSystem : MonoBehaviour
{

    ParticleSystem m_System;
    ParticleSystem.Particle[] m_Particles;
    public float m_Size = 1.0f;
    public float m_StartSpeed = 1.0f;

    private void LateUpdate()
    {
        InitializeIfNeeded();

        var main = m_System.main;

        // GetParticles is allocation free because we reuse the m_Particles buffer between updates
        int numParticlesAlive = m_System.GetParticles(m_Particles);

        float currentScale = (transform.localScale.x + transform.localScale.y + transform.localScale.z) / 3.0f;
        main.startSpeed = m_StartSpeed * currentScale;
        for (int i = 0; i < numParticlesAlive; i++)
        {
            m_Particles[i].startSize = currentScale;
        }

        m_System.SetParticles(m_Particles, numParticlesAlive);

    }

    void InitializeIfNeeded()
    {

        if (m_System == null)
            m_System = GetComponent<ParticleSystem>();

        var main = m_System.main;

        if (m_Particles == null || m_Particles.Length < main.maxParticles)
            m_Particles = new ParticleSystem.Particle[main.maxParticles];
    }
}
