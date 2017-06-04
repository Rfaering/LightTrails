using UnityEngine;
using System;

public class RenderManipulation : MonoBehaviour
{
    public Material[] materials = new Material[0];
    public int matIndex = -1;

    public ParticleSystemRenderMode RenderMode;

    // Update is called once per frame
    void Update()
    {
        if (matIndex > -1 && materials.Length > matIndex)
        {
            var renderer = GetComponent<Renderer>();
            renderer.material = materials[matIndex];
        }

        GetComponent<ParticleSystemRenderer>().renderMode = RenderMode;
    }
}
