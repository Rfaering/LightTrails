using System;
using UnityEngine;

public class ParticleEffect : RunningEffect
{
    public bool IsLooping = false;

    public float X;
    public float Y;

    public Vector2 Size;

    public Vector2 WidthRestrictions;
    public Vector2 HeightRestrictions;

    public bool CanChangePosition = false;

    public ParticleSystem ParticleSystem { get; private set; }

    public override void Initialize(float newLength)
    {
        ParticleSystem = GetComponent<ParticleSystem>();
        ParticleSystem.Simulate(0, true, true);

        IsLooping = false;

        Length = newLength;
        ElapsedTime = 0;

        if (WidthRestrictions == Vector2.zero)
        {
            WidthRestrictions = new Vector2(Size.x, Size.x);
        }
        if (HeightRestrictions == Vector2.zero)
        {
            HeightRestrictions = new Vector2(Size.y, Size.y);
        }
    }

    public virtual Vector2 SetSize(Vector2 size)
    {
        if (size.x <= WidthRestrictions.x && size.x >= WidthRestrictions.y)
        {
            Size.x = size.x;
        }
        else if (size.y <= HeightRestrictions.x && size.y >= HeightRestrictions.y)
        {
            Size.y = size.y;
        }

        return Size;
    }

    public Vector2 SetPosition(Vector2 position)
    {
        transform.position = new Vector3(position.x / 100.0f, position.y / 100.0f, transform.position.z);
        return position;
    }

    private void Update()
    {
        X = transform.position.x * 100.0f;
        Y = transform.position.y * 100.0f;
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