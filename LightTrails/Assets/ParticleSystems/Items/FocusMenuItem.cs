using Assets.Models;
using System.Collections.Generic;

public class FocusMenuItem : ParticleEffectMenuItem
{
    public override Attribute[] GetAttributes()
    {
        var focusEffect = assosicatedEffect.GetComponent<FocusEffect>();
        List<Attribute> fireAttributes = new List<Attribute>(base.GetAttributes())
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


        return fireAttributes.ToArray();
    }

    public void SetSpeed(float speed)
    {
        var focusEffect = assosicatedEffect.GetComponent<FocusEffect>();
        focusEffect.Speed = speed;
    }
}
