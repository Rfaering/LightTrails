using Assets.Models;
using System.Collections.Generic;

public class SmallFireMenuItem : ParticleEffectMenuItem
{
    public override List<Assets.Models.Attribute> GetAttributes()
    {
        List<Assets.Models.Attribute> attributes = new List<Assets.Models.Attribute>()
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
