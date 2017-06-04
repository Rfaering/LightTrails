using UnityEngine;

public class LightPlane : MonoBehaviour
{
    public int NeedForLightPlane = 0;

    internal void IncreaseNeedForLightPlane()
    {
        NeedForLightPlane++;
        UpdateMesh();
    }

    internal void DecreaseNeedForLightPlane()
    {
        NeedForLightPlane--;
        UpdateMesh();
    }

    private void UpdateMesh()
    {
        GetComponent<MeshRenderer>().enabled = NeedForLightPlane != 0;
    }
}
