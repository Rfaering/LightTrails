using Assets.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public class FireworksMenuItem : EffectMenuItem
{
    public override List<Assets.UI.Models.Attribute> GetAttributes()
    {
        var fireworksEffect = assosicatedEffect.GetComponent<FireworksEffects>();
        List<Assets.UI.Models.Attribute> fireAttributes = new List<Assets.UI.Models.Attribute>()
        {
            /*new OptionsAttribute()
            {
                Name = "Color",
                Options = Enum.GetNames(typeof(FireEffects.Color)).ToList(),
                SelectedValue = Enum.GetName(typeof(FireEffects.Color), FireEffects.Color.Red),
                CallBack = ChangeColor
            },*/
            new SliderAttribute()
            {
                Name = "Rotation",
                Min = -180,
                Max = 180,
                SelectedValue = 0,
                CallBack = SetRotation
            },
            new SliderAttribute()
            {
                Min = 1,
                Max = 30,
                SelectedValue = fireworksEffect.Intensity,
                Name = "Intensity",
                CallBack = SetItensity
            }
        };

        fireAttributes.AddRange(base.GetAttributes());

        return fireAttributes;
    }

    private void SetItensity(float value)
    {
        assosicatedEffect.GetComponent<FireworksEffects>().Intensity = value;
    }

    /*public void ChangeColor(string name)
    {
        var fireEffect = assosicatedEffect.GetComponent<FireEffects>();
        fireEffect.SelectedColor = (FireEffects.Color)Enum.Parse(typeof(FireEffects.Color), name);
    }*/
}
