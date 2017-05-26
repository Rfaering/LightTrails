using UnityEngine;
using System;

public class IntensityManipulation : MonoBehaviour
{
    [Range(1, 100)]
    public float Intensity = 10;

    // Update is called once per frame
    void Update()
    {
        var ps = GetComponent<ParticleSystem>();
        var emission = ps.emission;
        emission.rateOverTime = Intensity;
    }
}
