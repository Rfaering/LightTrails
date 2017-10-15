using System.Collections.Generic;
using Assets.Models;

public class MistMenuItem : ShaderAttributes
{
    public override List<Attribute> GetAttributes()
    {
        List<Attribute> attributes = new List<Attribute>()
        {
            new SliderAttribute()
            {
                Name = "Speed",
                CallBack = value => Material.SetFloat("_Speed", value),
                SelectedValue = 5,
                Min = 0,
                Max = 20
            }
        };

        attributes.AddRange(base.GetAttributes());
        //attributes.SetDefaultMaskValue("Mask08");
        return attributes;
    }
}

