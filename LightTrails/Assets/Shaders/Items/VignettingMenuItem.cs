using System.Collections.Generic;
using Assets.Models;

public class VignettingMenuItem : ShaderAttributes
{
    public override List<Attribute> GetAttributes()
    {
        List<Attribute> attributes = new List<Attribute>()
        {
            CreateShaderSliderAttribute("X", "_X", 0.0f, 1.0f),
            CreateShaderSliderAttribute("Y", "_Y", 0.0f, 1.0f),
            CreateShaderSliderAttribute("Intensity", "_Intensity", 0.1f, 2.0f),
            CreateShaderSliderAttribute("Size", "_Size", 0.5f, 2.0f)
        };

        attributes.AddRange(base.GetAttributes());

        return attributes;
    }
}

