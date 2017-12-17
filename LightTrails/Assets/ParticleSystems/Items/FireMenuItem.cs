using Assets.Models;
using System.Collections.Generic;

public class FireMenuItem : ParticleEffectMenuItem
{
    public enum Color { Red = 0, Green = 1, Blue = 2 }

    public override Attribute[] GetAttributes()
    {
        var attributes = base.GetAttributes();

        var value = assosicatedEffect.GetComponentInChildren<ColorManipulation>().colorOverLifeTimeIndex;

        List<Attribute> fireAttributes = new List<Attribute>(attributes)
        {
            new OptionsAttribute<Color>()
            {
                Name = "Color",
                SpecificCallBack = ChangeColor,
                SpecificSelectedValue = value != -1 ? (Color)value : Color.Red
            },
            new SliderAttribute()
            {
                Name = "Rotation",
                Min = -180,
                Max = 180,
                SelectedValue = assosicatedEffect.transform.localRotation.eulerAngles.z,
                CallBack = SetRotation
            }
        };

        return fireAttributes.ToArray();
    }

    public void ChangeColor(Color color)
    {
        var fireEffect = assosicatedEffect.GetComponentInChildren<ColorManipulation>();
        fireEffect.colorOverLifeTimeIndex = (int)color;
    }
}
