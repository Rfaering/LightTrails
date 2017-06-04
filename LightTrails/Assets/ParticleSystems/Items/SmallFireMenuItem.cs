using Assets.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public class SmallFireMenuItem : ParticleEffectMenuItem
{
    public override List<Assets.UI.Models.Attribute> GetAttributes()
    {
        List<Assets.UI.Models.Attribute> attributes = new List<Assets.UI.Models.Attribute>()
        {
            new ToggleAttribute()
            {
                Name = "Embers",
                SelectedValue = false,
                CallBack = value => { assosicatedEffect.GetComponentInChildren<SmallFireEffects>().Sparkles = value; }
            }
        };

        attributes.AddRange(base.GetAttributes());

        return attributes;
    }
}
