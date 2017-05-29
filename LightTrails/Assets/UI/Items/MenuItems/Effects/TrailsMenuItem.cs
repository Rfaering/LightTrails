using Assets.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public class TrailsMenuItem : EffectMenuItem
{
    public override List<Assets.UI.Models.Attribute> GetAttributes()
    {
        var value = assosicatedEffect.GetComponentInChildren<ParticleSystemManipulation>().IntensityValue;
        List<Assets.UI.Models.Attribute> attribtues = new List<Assets.UI.Models.Attribute>()
        {
            new SliderAttribute()
            {
                Min = 1,
                Max = 30,
                SelectedValue = value,
                Name = "Intensity",
                CallBack = SetIntensity
            },
            new ToggleAttribute()
            {
                SelectedValue = assosicatedEffect.GetComponent<ParticleSystemManipulation>().TurnOnLight,
                Name = "Lighting",
                CallBack = newValue => assosicatedEffect.GetComponent<ParticleSystemManipulation>().TurnOnLight = newValue
            } ,
            new ToggleAttribute()
            {
                SelectedValue = assosicatedEffect.GetComponent<ParticleSystemManipulation>().TurnOnNoise,
                Name = "Noise",
                CallBack = newValue => assosicatedEffect.GetComponent<ParticleSystemManipulation>().TurnOnNoise = newValue
            }
        };

        attribtues.AddRange(base.GetAttributes());

        return attribtues;
    }

    public void SetIntensity(float value)
    {
        var focusEffect = assosicatedEffect.GetComponent<ParticleSystemManipulation>();
        focusEffect.IntensityValue = value;
    }
}
