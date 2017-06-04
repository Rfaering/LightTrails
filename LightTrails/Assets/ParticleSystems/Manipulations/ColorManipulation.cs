using UnityEngine;
using System;

public class ColorManipulation : MonoBehaviour
{
    public Gradient[] colors = new Gradient[0];
    public int colorIndex = -1;

    public Gradient[] trailColor = new Gradient[0];
    public int trailColorIndex = -1;

    // Update is called once per frame
    void Update()
    {
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
