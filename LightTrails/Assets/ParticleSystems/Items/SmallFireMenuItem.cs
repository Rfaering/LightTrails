using Assets.Models;
using System.Collections.Generic;

public class SmallFireMenuItem : ParticleEffectMenuItem
{
    public override Attribute[] GetAttributes()
    {
        List<Attribute> attributes = new List<Attribute>(base.GetAttributes())
        {
            new ToggleAttribute()
            {
                Name = "Embers",
                SelectedValue = false,
                CallBack = value => { assosicatedEffect.GetComponentInChildren<SmallFireEffects>().Sparkles = value; }
            }
        };

        return attributes.ToArray();
    }
}
