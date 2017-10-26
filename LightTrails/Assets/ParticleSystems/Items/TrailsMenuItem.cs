using Assets.Models;
using System.Collections.Generic;

public class TrailsMenuItem : ParticleEffectMenuItem
{
    public enum Color { Orange = 0, Green = 1, Blue = 2 }

    public override Attribute[] GetAttributes()
    {
        var value = assosicatedEffect.GetComponentInChildren<SystemManipulation>().IntensityValue;
        List<Attribute> attributes = new List<Attribute>(base.GetAttributes())
        {
            new SliderAttribute()
            {
                Min = 1,
                Max = 30,
                SelectedValue = value,
                Name = "Intensity",
                CallBack = SetIntensity
            },
            new OptionsAttribute<Color>()
            {
                Name = "Color",
                SpecificCallBack = newValue =>
                {
                    assosicatedEffect.GetComponent<ColorManipulation>().trailColorIndex = (int)newValue;
                    assosicatedEffect.GetComponent<ColorManipulation>().colorIndex = (int)newValue;
                },
                SpecificSelectedValue = Color.Orange,
            },
            new ToggleAttribute()
            {
                SelectedValue = assosicatedEffect.GetComponent<SystemManipulation>().TurnOnLight,
                Name = "Lighting",
                CallBack = newValue => {

                    if (newValue)
                    {
                        FindObjectOfType<LightPlane>().IncreaseNeedForLightPlane();
                    } else
                    {
                        FindObjectOfType<LightPlane>().DecreaseNeedForLightPlane();
                    }

                    assosicatedEffect.GetComponent<SystemManipulation>().TurnOnLight = newValue;
                }
            },
            new ToggleAttribute()
            {
                SelectedValue = assosicatedEffect.GetComponent<SystemManipulation>().TurnOnNoise,
                Name = "Noise",
                CallBack = newValue => assosicatedEffect.GetComponent<SystemManipulation>().TurnOnNoise = newValue
            }
        };

        return attributes.ToArray();
    }

    public void SetIntensity(float value)
    {
        var focusEffect = assosicatedEffect.GetComponent<SystemManipulation>();
        focusEffect.IntensityValue = value;
    }
}
