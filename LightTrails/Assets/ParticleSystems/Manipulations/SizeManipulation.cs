using UnityEngine;
using System;

public class SizeManipulation : MonoBehaviour
{
    public float Min = 1;
    public float Max = 1;

    // Update is called once per frame
    void Update()
    {
        var main = GetComponent<ParticleSystem>().main;
        main.startSize = new ParticleSystem.MinMaxCurve(Min, Max);
    }
}
