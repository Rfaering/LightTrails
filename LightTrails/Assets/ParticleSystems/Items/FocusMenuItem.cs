using Assets.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public class FocusMenuItem : ParticleEffectMenuItem
{
    public override List<Assets.UI.Models.Attribute> GetAttributes()
    {
        var focusEffect = assosicatedEffect.GetComponent<FocusEffect>();
        List<Assets.UI.Models.Attribute> fireAttributes = new List<Assets.UI.Models.Attribute>()
        {
            new SliderAttribute()
            {
                Min = 1,
                Max = 3,
                SelectedValue = focusEffect.Speed,
                Name = "Speed",
                CallBack = SetSpeed
            }
        };

        fireAttributes.AddRange(base.GetAttributes());

        return fireAttributes;
    }

    public void SetSpeed(float speed)
    {
        var focusEffect = assosicatedEffect.GetComponent<FocusEffect>();
        focusEffect.Speed = speed;
    }
}
