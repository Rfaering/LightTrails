using UnityEngine;
using UnityEngine.UI;

public class ShaderEffect : MonoBehaviour
{
    public Material Material;
    public bool TimeRunning;

    // Use this for initialization
    void Start()
    {
        Material = GetComponent<RawImage>().material;
        TimeRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeRunning)
        {
            var inputTime = Material.GetFloat("_InputTime");
            inputTime += Time.deltaTime;
            Material.SetFloat("_InputTime", inputTime);
        }
    }
}
