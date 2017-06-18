using UnityEngine;
using System;

public class ColorManipulation : MonoBehaviour
{
    public Gradient[] colors = new Gradient[0];
    public int colorIndex = -1;

    public Gradient[] colorOverLifetime = new Gradient[0];
    public int colorOverLifeTimeIndex = -1;

    public Gradient[] trailColor = new Gradient[0];
    public int trailColorIndex = -1;

    // Update is called once per frame
    void Update()
    {
        if (colorOverLifeTimeIndex > -1 && colorOverLifetime.Length > colorOverLifeTimeIndex)
        {
            var ps = GetComponent<ParticleSystem>();
            var main = ps.colorOverLifetime;
            main.color = colorOverLifetime[colorOverLifeTimeIndex];
        }

        if (colorIndex > -1 && colors.Length > colorIndex)
        {
            var ps = GetComponent<ParticleSystem>();
            var main = ps.main;
            main.startColor = colors[colorIndex];
        }

        if (trailColorIndex > -1 && trailColor.Length > trailColorIndex)
        {
            var ps = GetComponent<ParticleSystem>();
            var main = ps.trails;
            main.colorOverTrail = trailColor[trailColorIndex];
        }
    }
}
