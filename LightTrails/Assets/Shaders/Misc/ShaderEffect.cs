using UnityEngine;
using UnityEngine.UI;

public class ShaderEffect : RunningEffect
{
    public Material Material;

    private void Update()
    {
        Material = GetComponent<RawImage>().material;
    }

    public override void Initialize(float newLength)
    {
        Length = newLength;
        ElapsedTime = 0;

        if (Material != null)
        {
            Material.SetFloat("_InputTime", 0);
        }
    }

    public override void Progress(float newElapsedTime)
    {
        ElapsedTime = newElapsedTime;

        if (Material != null)
        {
            Material.SetFloat("_InputTime", ElapsedTime);
        }
    }
}
