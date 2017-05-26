using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WispsEffects : MonoBehaviour
{
    public enum Color { Red, Green, Blue, White, Pink }
    public Color color;

    // Update is called once per frame
    void Update()
    {
        var main = GetComponent<ParticleSystem>().main;
        switch (color)
        {
            case Color.Red:
                main.startColor = MinMaxColor("#FF6500FF", "#FF1F00FF");
                break;
            case Color.Green:
                main.startColor = MinMaxColor("#00FF17FF", "#008F37FF");
                break;
            case Color.White:
                main.startColor = MinMaxColor("#FFFFFFFF", "#FFFFFFFF");
                break;
            case Color.Blue:
                main.startColor = MinMaxColor("#38A5FFFF", "#0048ABFF");
                break;
            case Color.Pink:
                main.startColor = MinMaxColor("#FF2FFBFF", "#FF178FFF");
                break;
            default:
                break;
        }
    }

    private ParticleSystem.MinMaxGradient MinMaxColor(string min, string max)
    {
        return new ParticleSystem.MinMaxGradient(GetColor(min), GetColor(max));
    }

    public UnityEngine.Color GetColor(string html)
    {
        var color = UnityEngine.Color.white;
        ColorUtility.TryParseHtmlString(html, out color);
        return color;
    }
}
