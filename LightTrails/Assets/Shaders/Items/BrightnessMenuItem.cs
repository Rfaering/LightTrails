using System.Collections.Generic;
using Assets.Models;

public class BrightnessMenuItem : ShaderAttributes
{
    public override List<Attribute> GetAttributes()
    {
        List<Attribute> attributes = new List<Attribute>()
        {
            new SliderAttribute()
            {
                Name = "Brightness",
                CallBack = value => Material.SetFloat("_Brightness", value),
                SelectedValue = Material.GetFloat("_Brightness"),
                Min = 0.05f,
                Max = 1.0f
            }
        };

        attributes.AddRange(base.GetAttributes());

        return attributes;
    }
}

