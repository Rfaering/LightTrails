using Assets.Models;
using System.Collections.Generic;

public class PlexMenuItem : ParticleEffectMenuItem
{
    public enum Color { White, BlueGreen, RedOrange, PurplePink }

    public override Attribute[] GetAttributes()
    {
        List<Attribute> attributes = new List<Attribute>(base.GetAttributes())
        {
            new SliderAttribute()
            {
                Min = 1,
                Max = 2,
                Name = "Lines",
                CallBack = value => { assosicatedEffect.GetComponentInChildren<ParticlePlex>().MaxDistance = value; },
                SelectedValue = assosicatedEffect.GetComponentInChildren<ParticlePlex>().MaxDistance
            },
            new OptionsAttribute<Color>()
            {
                Name = "Color",
                SpecificCallBack = color => assosicatedEffect.GetComponentInChildren<ColorManipulation>().colorIndex = (int)color,
                SpecificSelectedValue = Color.White
            },
        };
        
        return attributes.ToArray();
    }
}
