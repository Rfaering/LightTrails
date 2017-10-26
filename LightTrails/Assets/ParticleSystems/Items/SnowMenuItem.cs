using Assets.Models;
using System.Collections.Generic;

public class SnowMenuItem : ParticleEffectMenuItem
{
    public override Attribute[] GetAttributes()
    {
        List<Attribute> attribtues = new List<Attribute>(base.GetAttributes())
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

        return attribtues.ToArray();
    }
}
