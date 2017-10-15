using System.Collections.Generic;
using Assets.Models;

public class DistortionMenuItem : ShaderAttributes
{
    public override List<Attribute> GetAttributes()
    {
        List<Attribute> attributes = new List<Attribute>()
        {
            /*new ToggleAttribute()
            {
                Name = "Paint",
                SelectedValue = false,
                CallBack = value => TogglePaint(value)
            }*/
            new SliderAttribute()
            {
                Name = "Intensity",
                CallBack = value => Material.SetFloat("_Intensity", value),
                SelectedValue = 50,
                Min = 10,
                Max = 100
            },
            new SliderAttribute()
            {
                Name = "Speed",
                CallBack = value => Material.SetFloat("_Speed", value),
                SelectedValue = 3,
                Min = 0,
                Max = 5
            }
        };

        attributes.AddRange(base.GetAttributes());

        return attributes;
    }
}

