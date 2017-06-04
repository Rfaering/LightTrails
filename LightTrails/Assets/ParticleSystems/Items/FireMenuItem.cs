using Assets.UI.Models;
using System.Collections.Generic;

public class FireMenuItem : ParticleEffectMenuItem
{
    public override List<Attribute> GetAttributes()
    {
        List<Attribute> fireAttributes = new List<Attribute>()
        {
            new OptionsAttribute<FireEffects.Color>()
            {
                Name = "Color",
                SpecificCallBack = ChangeColor,
                SpecificSelectedValue = FireEffects.Color.Red
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

    public void ChangeColor(FireEffects.Color color)
    {
        var fireEffect = assosicatedEffect.GetComponent<FireEffects>();
        fireEffect.SelectedColor = color;
    }
}
