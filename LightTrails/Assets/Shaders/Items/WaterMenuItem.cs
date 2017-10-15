using System.Collections.Generic;
using Assets.Models;

public class WaterMenuItem : ShaderAttributes
{
    public override List<Attribute> GetAttributes()
    {
        List<Attribute> attributes = new List<Attribute>()
        {
            new SliderAttribute()
            {
                Name = "Zoom",
                CallBack = value => Material.SetFloat("_Zoom", value),
                SelectedValue = Material.GetFloat("_Zoom"),
                Min = 1.0f,
                Max = 3.0f
            },
            new SliderAttribute()
            {
                Name = "Speed",
                CallBack = value => Material.SetFloat("_Speed", value),
                SelectedValue = Material.GetFloat("_Speed"),
                Min = 1.0f,
                Max = 3.0f
            },
            new SliderAttribute()
            {
                Name = "Distort",
                CallBack = value => Material.SetFloat("_WaterDistortion", value),
                SelectedValue = Material.GetFloat("_WaterDistortion"),
                Min = 0.0f,
                Max = 2.0f
            }
        };

        attributes.AddRange(base.GetAttributes());
        //attributes.SetDefaultMaskValue("Mask01");

        return attributes;
    }
}

