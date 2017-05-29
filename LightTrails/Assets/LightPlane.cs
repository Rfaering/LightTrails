using UnityEngine;

public class LightPlane : MonoBehaviour
{
    internal void SetEnabled(bool value)
    {
        GetComponent<MeshRenderer>().enabled = value;
    }
}
