using Assets.Models;
using System.Collections.Generic;

public class FireworksMenuItem : ParticleEffectMenuItem
{
    public override Attribute[] GetAttributes()
    {
        var fireworksEffect = assosicatedEffect.GetComponent<FireworksEffects>();
        List<Attribute> fireAttributes = new List<Attribute>(base.GetAttributes())
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
                Min = 1,
                Max = 30,
                SelectedValue = fireworksEffect.Intensity,
                Name = "Intensity",
                CallBack = SetItensity
            }
        };


        return fireAttributes.ToArray();
    }

    private void SetItensity(float value)
    {
        assosicatedEffect.GetComponent<FireworksEffects>().Intensity = value;
    }

    /*public void ChangeColor(string name)
    {
        var fireEffect = assosicatedEffect.GetComponent<FireEffects>();
        fireEffect.SelectedColor = (FireEffects.Color)Enum.Parse(typeof(FireEffects.Color), name);
    }*/
}
