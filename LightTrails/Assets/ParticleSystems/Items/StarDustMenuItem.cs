using Assets.UI.Models;
using System.Collections.Generic;

public class StartDustMenuItem : ParticleEffectMenuItem
{
    public override List<Attribute> GetAttributes()
    {
        List<Attribute> attribtues = new List<Attribute>()
        {
            new SliderAttribute()
            {
                Min = 1,
                Max = 100,
                SelectedValue = assosicatedEffect.GetComponentInChildren<SystemManipulation>().IntensityValue,
                Name = "Intensity",
                CallBack = newValue => assosicatedEffect.GetComponentInChildren<SystemManipulation>().IntensityValue = newValue
            },
            new SliderAttribute()
            {
                Name = "Rotation",
                Min = -180,
                Max = 180,
                SelectedValue = 0,
                CallBack = SetRotation
            },
            new OptionsAttribute<StarDustEffect.Output>()
            {
                Name = "Color",
                SpecificCallBack = newValue => assosicatedEffect.GetComponentInChildren<StarDustEffect>().CurrentOutput = newValue,
                SpecificSelectedValue = assosicatedEffect.GetComponentInChildren<StarDustEffect>().CurrentOutput,
            },
        };

        attribtues.AddRange(base.GetAttributes());

        return attribtues;
    }
}
