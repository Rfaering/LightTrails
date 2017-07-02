using Assets.Models;
using System.Collections.Generic;

public class FocusMenuItem : ParticleEffectMenuItem
{
    public override List<Attribute> GetAttributes()
    {
        var focusEffect = assosicatedEffect.GetComponent<FocusEffect>();
        List<Attribute> fireAttributes = new List<Attribute>()
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
