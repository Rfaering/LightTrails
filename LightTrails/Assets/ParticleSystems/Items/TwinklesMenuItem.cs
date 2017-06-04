using Assets.UI.Models;
using System.Collections.Generic;

public class TwinklesMenuItem : ParticleEffectMenuItem
{
    public override List<Attribute> GetAttributes()
    {
        List<Attribute> attribtues = new List<Assets.UI.Models.Attribute>()
        {
            new SliderAttribute()
            {
                Name = "Rotation",
                Min = -180,
                Max = 180,
                SelectedValue = 0,
                CallBack = SetRotation
            },
        };

        attribtues.AddRange(base.GetAttributes());

        return attribtues;
    }
}
