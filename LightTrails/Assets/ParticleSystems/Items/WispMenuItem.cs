using Assets.Models;
using System.Collections.Generic;
using System.Linq;

public class WispMenuItem : ParticleEffectMenuItem
{
    public override Attribute[] GetAttributes()
    {
        var value = assosicatedEffect.GetComponentInChildren<SystemManipulation>().IntensityValue;
        List<Attribute> attribtues = new List<Attribute>(base.GetAttributes())
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
                Options = System.Enum.GetNames(typeof(WispsEffects.Color)).ToList(),
                SelectedValue = System.Enum.GetName(typeof(WispsEffects.Color), WispsEffects.Color.Red),
                CallBack = ChangeColor
            }
        };

        return attribtues.ToArray();
    }

    public void ChangeColor(string name)
    {
        var wispSetColor = assosicatedEffect.GetComponentInChildren<WispsEffects>();
        wispSetColor.color = (WispsEffects.Color)System.Enum.Parse(typeof(WispsEffects.Color), name);
    }

    public void SetIntensity(float value)
    {
        var focusEffect = assosicatedEffect.GetComponentInChildren<SystemManipulation>();
        focusEffect.IntensityValue = value;
    }
}
