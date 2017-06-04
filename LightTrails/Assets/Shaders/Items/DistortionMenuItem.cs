using System.Collections.Generic;
using Assets.UI.Models;
using UnityEngine.UI;

public class DistortionMenuItem : ShaderEffectMenuItem
{
    public override List<Attribute> GetAttributes()
    {
        var material = assosicatedEffect.GetComponent<RawImage>().material;

        List<Attribute> attributes = new List<Attribute>()
        {
            /*new ToggleAttribute()
            {
                Name = "Paint",
                SelectedValue = false,
                CallBack = value => TogglePaint(value)
            }*/
            new SliderAttribute()
            {
                Name = "Intensity",
                CallBack = value => material.SetFloat("_Intensity", value),
                SelectedValue = 50,
                Min = 10,
                Max = 100
            },
            new SliderAttribute()
            {
                Name = "Speed",
                CallBack = value => material.SetFloat("_Speed", value),
                SelectedValue = 3,
                Min = 0,
                Max = 5
            }
        };

        attributes.AddRange(base.GetAttributes());

        return attributes;
    }

    public void TogglePaint(bool newValue)
    {
        if (newValue)
        {
            assosicatedEffect.GetComponent<PaintScript>().StartPaintMode();
        }
        else
        {
            assosicatedEffect.GetComponent<PaintScript>().StopPaintMode();
        }
    }
}

