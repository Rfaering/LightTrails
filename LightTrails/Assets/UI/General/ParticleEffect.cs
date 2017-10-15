﻿using System;
using UnityEngine;

public class ParticleEffect : RunningEffect
{
    public bool IsLooping = false;

    public ParticleSystem ParticleSystem { get; private set; }

    public override void Initialize(float newLength)
    {
        ParticleSystem = GetComponent<ParticleSystem>();
        ParticleSystem.Simulate(0, true, true);

        IsLooping = false;

        Length = newLength;
        ElapsedTime = 0;
    }

    private void SetDefaultValues(GameObject gameObject, float length)
    {
        /*var allParticleSystems = gameObject.GetComponentsInChildren<ParticleSystem>(true);
        var maxDuration = allParticleSystems.Max(x => x.main.startLifetime.constantMax);
        foreach (var ps in allParticleSystems)
        {
            ps.Clear();
            ps.Stop();
            ps.randomSeed = 42;

            var main = ps.main;
            main.loop = false;
            main.duration = length - maxDuration;
        }*/
    }

    public override void Progress(float newElapsedTime)
    {
        var deltaTime = newElapsedTime - ElapsedTime;

        if (IsLooping)
        {
            /*if (ElapsedTime > (Length / 2.0f))
            {
                EnsureCorrectActiveState(FirstHalf, true);
                EnsureCorrectActiveState(SecondHalf, true);

                FirstHalf.Simulate(newElapsedTime + Length / 2.0f, true, true);
                SecondHalf.Simulate(newElapsedTime - Length / 2.0f, true, true);
            }
            else
            {
                EnsureCorrectActiveState(FirstHalf, true);
                EnsureCorrectActiveState(SecondHalf, false);
                FirstHalf.Simulate(newElapsedTime + Length / 2.0f, true, true);
            }*/
        }
        else
        {
            if (deltaTime >= 0)
            {
                ParticleSystem.Simulate(deltaTime, true, false);
            }
            else
            {
                ParticleSystem.Simulate(newElapsedTime, true, true);
            }
        }

        ElapsedTime = newElapsedTime;
    }

    private void EnsureCorrectActiveState(ParticleSystem ps, bool enabled)
    {
        if (ps.gameObject.activeSelf != enabled)
        {
            ps.gameObject.SetActive(enabled);
        }
    }
}