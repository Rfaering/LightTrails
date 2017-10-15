using System.Collections.Generic;
using UnityEngine;
using Assets.Models;

public class BarsMenuItem : ShaderAttributes
{
    public override List<Attribute> GetAttributes()
    {
        List<Attribute> attributes = new List<Attribute>()
        {
            new SliderAttribute()
            {
                Name = "Addition",
                CallBack = value => Material.SetFloat("_Add", value),
                SelectedValue = 0.25f,
                Min = -1,
                Max = 1
            },
            new SliderAttribute()
            {
                Name = "Intensity",
                CallBack = value => Material.SetFloat("_Bars", value),
                SelectedValue = 50.0f,
                Min = 1,
                Max = 200
            },
            new SliderAttribute()
            {
                Name = "Spread",
                CallBack = value => Material.SetFloat("_Distance", value),
                SelectedValue = 3,
                Min = 2,
                Max = 5
            },
            new SliderAttribute()
            {
                Name = "Speed",
                CallBack = value => Material.SetFloat("_Speed", value),
                SelectedValue = 0.5f,
                Min = 0,
                Max = 5
            }
        };

        attributes.AddRange(base.GetAttributes());

        return attributes;
    }
}

