using Assets.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public class WispMenuItem : EffectMenuItem
{
    public override List<Assets.UI.Models.Attribute> GetAttributes()
    {
        var value = assosicatedEffect.GetComponentInChildren<IntensityManipulation>().Intensity;
        List<Assets.UI.Models.Attribute> attribtues = new List<Assets.UI.Models.Attribute>()
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
        var focusEffect = assosicatedEffect.GetComponentInChildren<IntensityManipulation>();
        focusEffect.Intensity = value;
    }
}
