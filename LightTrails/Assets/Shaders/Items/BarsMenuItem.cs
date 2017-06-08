﻿using System.Collections.Generic;
using Assets.UI.Models;
using UnityEngine;
using UnityEngine.UI;

public class BarsMenuItem : ShaderEffectMenuItem
{
    public override List<Attribute> GetAttributes()
    {
        var material = assosicatedEffect.GetComponent<RawImage>().material;
        List<Attribute> attributes = new List<Attribute>()
        {
            new SliderAttribute()
            {
                Name = "Addition",
                CallBack = value => material.SetFloat("_Add", value),
                SelectedValue = 0.25f,
                Min = -1,
                Max = 1
            },
            new SliderAttribute()
            {
                Name = "Intensity",
                CallBack = value => material.SetFloat("_Bars", value),
                SelectedValue = 50.0f,
                Min = 1,
                Max = 200
            },
            new SliderAttribute()
            {
                Name = "Spread",
                CallBack = value => material.SetFloat("_Distance", value),
                SelectedValue = 3,
                Min = 2,
                Max = 5
            },
            new SliderAttribute()
            {
                Name = "Speed",
                CallBack = value => material.SetFloat("_Speed", value),
                SelectedValue = 0.5f,
                Min = 0,
                Max = 5
            }
        };

        attributes.AddRange(base.GetAttributes());

        return attributes;
    }

    public void TogglePaint(bool newValue)
    {
        if (newValue)
        {
            assosicatedEffect.GetComponent<PaintScript>().StartPaintMode();
        }
        else
        {
            assosicatedEffect.GetComponent<PaintScript>().StopPaintMode();
        }
    }
}
