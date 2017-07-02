using Assets.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public class WispMenuItem : ParticleEffectMenuItem
{
    public override List<Assets.Models.Attribute> GetAttributes()
    {
        var value = assosicatedEffect.GetComponentInChildren<SystemManipulation>().IntensityValue;
        List<Assets.Models.Attribute> attribtues = new List<Assets.Models.Attribute>()
        {
            new SliderAttribute()
            {
                Min = 1,
                Max = 25,
                SelectedValue = value,
                Name = "Intensity",
                CallBack = SetIntensity
            },
            new OptionsAttribute()
            {
                Name = "Color",
                Options = Enum.GetNames(typeof(WispsEffects.Color)).ToList(),
                SelectedValue = Enum.GetName(typeof(WispsEffects.Color), WispsEffects.Color.Red),
                CallBack = ChangeColor
            }
        };

        attribtues.AddRange(base.GetAttributes());

        return attribtues;
    }

    public void ChangeColor(string name)
    {
        var wispSetColor = assosicatedEffect.GetComponentInChildren<WispsEffects>();
        wispSetColor.color = (WispsEffects.Color)Enum.Parse(typeof(WispsEffects.Color), name);
    }

    public void SetIntensity(float value)
    {
        var focusEffect = assosicatedEffect.GetComponentInChildren<SystemManipulation>();
        focusEffect.IntensityValue = value;
    }
}
