using Assets.Models;
using System.Collections.Generic;

public class ExplosionMenuItem : ParticleEffectMenuItem
{
    public override Attribute[] GetAttributes()
    {
        List<Attribute> attributes = new List<Attribute>(base.GetAttributes())
        {
            new SliderAttribute()
            {
                Min = 1,
                Max = 100,
                SelectedValue = assosicatedEffect.GetComponent<SystemManipulation>().IntensityValue,
                Name = "Intensity",
                CallBack = newValue => assosicatedEffect.GetComponent<SystemManipulation>().IntensityValue = newValue
            },
            new SliderAttribute()
            {
                Min = 0.1f,
                Max = 4.0f,
                SelectedValue = assosicatedEffect.GetComponent<ExplosionEffects>().Radius,
                Name = "Radius",
                CallBack = newValue => assosicatedEffect.GetComponent<ExplosionEffects>().Radius = newValue
            },
            new ToggleAttribute()
            {
                SelectedValue = assosicatedEffect.GetComponent<SystemManipulation>().TurnOnLight,
                Name = "Lighting",
                CallBack = newValue => {

                    if (newValue)
                    {
                        FindObjectOfType<LightPlane>().IncreaseNeedForLightPlane();
                    } else
                    {
                        FindObjectOfType<LightPlane>().DecreaseNeedForLightPlane();
                    }

                    assosicatedEffect.GetComponent<SystemManipulation>().TurnOnLight = newValue;
                }
            },
        };

        return attributes.ToArray();
    }
}
