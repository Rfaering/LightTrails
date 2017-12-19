using Assets.Models;
using System.Collections.Generic;
using UnityEngine;

public class WindMenuItem : ParticleEffectMenuItem
{
    public enum Color { Orange = 0, Green = 1, Blue = 2, Red = 3 }
    public enum Figures { Blobs = 0, Bubbles = 1, Leafs = 2, Hearts = 3 }

    public override Attribute[] GetAttributes()
    {
        List<Attribute> attributes = new List<Attribute>(base.GetAttributes())
        {
            new SliderAttribute()
            {
                Min = 1,
                Max = 100,
                SelectedValue = assosicatedEffect.GetComponentInChildren<SystemManipulation>().IntensityValue,
                Name = "Intensity",
                CallBack = newValue => assosicatedEffect.GetComponentInChildren<SystemManipulation>().IntensityValue = newValue
            },
            new OptionsAttribute<Color>()
            {
                Name = "Color",
                SpecificCallBack = newValue =>
                {
                    assosicatedEffect.GetComponentInChildren<ColorManipulation>().trailColorIndex = (int)newValue;
                    assosicatedEffect.GetComponentInChildren<ColorManipulation>().colorIndex = (int)newValue;
                },
                SpecificSelectedValue = Color.Orange,
            },
            new OptionsAttribute<Figures>()
            {
                Name = "Color",
                SpecificCallBack = newValue =>
                {
                    var render = assosicatedEffect.GetComponentInChildren<RenderManipulation>();
                    render.matIndex = (int)newValue;

                    var size = assosicatedEffect.GetComponentInChildren<SizeManipulation>();
                    if(newValue == 0)
                    {
                        render.RenderMode = ParticleSystemRenderMode.Stretch;
                        size.Min = 0.5f;
                        size.Max = 1.0f;
                    } else
                    {
                        render.RenderMode = ParticleSystemRenderMode.Billboard;
                        size.Min = 0.4f;
                        size.Max = 0.8f;
                    }
                },
                SpecificSelectedValue = Figures.Blobs,
            },
        };

        return attributes.ToArray();
    }
}
