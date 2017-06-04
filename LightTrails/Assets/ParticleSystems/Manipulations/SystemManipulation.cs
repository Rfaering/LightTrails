using UnityEngine;
using System;

public class SystemManipulation : MonoBehaviour
{
    public float IntensityControl = 1;

    [Range(1, 100)]
    public float IntensityValue = 10;

    public bool TurnOnLight = false;

    public bool TurnOnTrails = false;

    public bool TurnOnNoise = false;

    // Update is called once per frame
    void Update()
    {
        var ps = GetComponent<ParticleSystem>();
        var emission = ps.emission;
        emission.rateOverTime = IntensityValue / IntensityControl;

        var lights = ps.lights;
        lights.enabled = TurnOnLight;

        var trails = ps.trails;
        trails.enabled = TurnOnTrails;        

        var noise = ps.noise;
        noise.enabled = TurnOnNoise;
    }
}
