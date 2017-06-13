using UnityEngine;
using System;

public class FireworksEffects : MonoBehaviour
{
    [Range(1, 30)]
    public float Intensity = 10;

    public enum Color { Blue, Red, Green }

    public Color SelectedColor;

    private void Start()
    {
        SelectedColor = Color.Red;
    }

    // Update is called once per frame
    void Update()
    {
        var ps = transform.GetChild(0).GetComponent<ParticleSystem>();
        var emission = ps.emission;
        emission.rateOverTime = Intensity;

        if (SelectedColor == Color.Blue)
        {
            var main = ps.main;
            main.startColor = UnityEngine.Color.blue;
        }
    }
}
