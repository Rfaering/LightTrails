using Assets.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public class FireMenuItem : EffectMenuItem
{
    public override List<Assets.UI.Models.Attribute> GetAttributes()
    {
        List<Assets.UI.Models.Attribute> fireAttributes = new List<Assets.UI.Models.Attribute>()
        {
            new OptionsAttribute()
            {
                Name = "Color",
                Options = Enum.GetNames(typeof(FireEffects.Color)).ToList(),
                SelectedValue = Enum.GetName(typeof(FireEffects.Color), FireEffects.Color.Red),
                CallBack = ChangeColor
            },
            new SliderAttribute()
            {
                Name = "Rotation",
                Min = -180,
                Max = 180,
                SelectedValue = 0,
                CallBack = SetRotation
            }
        };

        fireAttributes.AddRange(base.GetAttributes());

        return fireAttributes;
    }

    public void ChangeColor(string name)
    {
        var fireEffect = assosicatedEffect.GetComponent<FireEffects>();
        fireEffect.SelectedColor = (FireEffects.Color)Enum.Parse(typeof(FireEffects.Color), name);
    }
}
