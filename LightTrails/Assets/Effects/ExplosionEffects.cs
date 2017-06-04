using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffects : MonoBehaviour
{    
    [Range(0.1f, 4.0f)]
    public float Radius = 0.1f;

    void Start()
    {

    }

    void Update()
    {
        var ps = GetComponent<ParticleSystem>();
        var shape = ps.shape;
        shape.radius = Radius;
    }
}
