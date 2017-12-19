using System.Collections.Generic;
using Assets.Models;

public class WaveMenuItem : ShaderAttributes
{
    public override List<Attribute> GetAttributes()
    {
        List<Attribute> attributes = new List<Attribute>()
        {
            CreateShaderSliderAttribute("X", "_X", 0.0f, 1.0f),
            CreateShaderSliderAttribute("Y", "_Y", 0.0f, 1.0f),
            CreateShaderSliderAttribute("Intensity", "_Intensity", 1.0f, 20.0f)
        };

        attributes.AddRange(base.GetAttributes());

        return attributes;
    }
}

