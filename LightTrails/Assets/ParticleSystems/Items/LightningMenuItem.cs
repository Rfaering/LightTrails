using Assets.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public class LightningMenuItem : ParticleEffectMenuItem
{
    public override List<Assets.UI.Models.Attribute> GetAttributes()
    {
        List<Assets.UI.Models.Attribute> attribtues = new List<Assets.UI.Models.Attribute>()
        {
            /*new SliderAttribute()
            {
                Name = "Rotation",
                Min = -180,
                Max = 180,
                SelectedValue = 0,
                CallBack = SetRotation
            },
            new SliderAttribute()
            {
                Name = "Scale",
                Min = 1,
                Max = 3,
                SelectedValue = 1,
                CallBack = SetScale
            },*/
        };

        attribtues.AddRange(base.GetAttributes());

        return attribtues;
    }
}
