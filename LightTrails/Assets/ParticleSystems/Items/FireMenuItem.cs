using Assets.Models;
using System.Collections.Generic;

public class FireMenuItem : ParticleEffectMenuItem
{
    public enum Color { Red = 0, Green = 1, Blue = 2 }

    public override Attribute[] GetAttributes()
    {
        List<Attribute> fireAttributes = new List<Attribute>()
        {
            new OptionsAttribute<Color>()
            {
                Name = "Color",
                SpecificCallBack = ChangeColor,
                SpecificSelectedValue = Color.Red
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

        return fireAttributes.ToArray();
    }

    public void ChangeColor(Color color)
    {
        var fireEffect = assosicatedEffect.GetComponentInChildren<ColorManipulation>();
        fireEffect.colorOverLifeTimeIndex = (int)color;
    }
}
