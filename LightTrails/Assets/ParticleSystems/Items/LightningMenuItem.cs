using Assets.Models;
using System.Collections.Generic;

public class LightningMenuItem : ParticleEffectMenuItem
{
    public override Attribute[] GetAttributes()
    {
        List<Attribute> attribtues = new List<Assets.Models.Attribute>(base.GetAttributes())
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

        return attribtues.ToArray();
    }
}
