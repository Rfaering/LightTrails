using UnityEngine;
using System.Collections;

public class EmitterResizeableParticleEffect : ParticleEffect
{
    public ParticleSystem System;

    public override Vector2 SetSize(Vector2 size)
    {
        var shape = System.shape;
        shape.scale = new Vector3(size.x / 100.0f, 0.5f, 5.0f);

        return base.SetSize(size);
    }
}
