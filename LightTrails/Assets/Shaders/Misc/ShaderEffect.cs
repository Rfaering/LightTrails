using System;
using UnityEngine;
using UnityEngine.UI;

public class ShaderEffect : RunningEffect
{
    public Material Material;

    // Use this for initialization
    void Start()
    {
        Material = GetComponent<RawImage>().material;
    }

    public override void Initialize(float newLength)
    {
        Length = newLength;
        ElapsedTime = 0;
        Material.SetFloat("_InputTime", 0);
    }

    public override void Progress(float newElapsedTime)
    {
        ElapsedTime = newElapsedTime;
        Material.SetFloat("_InputTime", ElapsedTime);
    }
}
