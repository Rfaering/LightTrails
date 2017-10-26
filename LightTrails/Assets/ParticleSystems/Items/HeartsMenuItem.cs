using Assets.Models;
using System.Collections.Generic;

public class HeartsMenuItem : ParticleEffectMenuItem
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
            new SliderAttribute()
            {
                Min = 0.1f,
                Max = 3,
                SelectedValue = assosicatedEffect.GetComponentInChildren<HeartEffects>().Intensity,
                Name = "Intensity",
                CallBack = SetItensity
            }
        };
        
        return attribtues.ToArray();
    }

    private void SetItensity(float value)
    {
        assosicatedEffect.GetComponentInChildren<HeartEffects>().Intensity = value;
    }
}
