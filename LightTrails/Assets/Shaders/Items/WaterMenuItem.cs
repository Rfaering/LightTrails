using System.Collections.Generic;
using Assets.Models;

public class WaterMenuItem : ShaderEffectMenuItem
{
    public override List<Attribute> GetAttributes()
    {
        List<Attribute> attributes = new List<Attribute>()
        {
            new SliderAttribute()
            {
                Name = "Zoom",
                CallBack = value => Material.SetFloat("_Zoom", value),
                SelectedValue = 1.0f,
                Min = 1.0f,
                Max = 3.0f
            },
            new SliderAttribute()
            {
                Name = "Speed",
                CallBack = value => Material.SetFloat("_Speed", value),
                SelectedValue = 2.0f,
                Min = 1.0f,
                Max = 3.0f
            },
            new ToggleAttribute()
            {
                Name = "Distort",
                CallBack = value => Material.SetFloat("_WaterDistortion", value ? 1.0f : 0.0f),
                SelectedValue = false
            }
        };

        attributes.AddRange(base.GetAttributes());
        attributes.SetDefaultMaskValue("Mask01");

        return attributes;
    }
}

