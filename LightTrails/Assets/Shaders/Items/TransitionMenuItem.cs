using System.Collections.Generic;
using Assets.Models;
using UnityEngine;
using System.Linq;

public class TransitionMenuItem : ShaderAttributes
{
    public string TransitionMask = "Mask01";

    public override List<Attribute> GetAttributes()
    {
        List<Attribute> attributes = new List<Attribute>()
        {
            CreateShaderSliderAttribute("Length", "_Length", 1.0f, 5.0f),
            CreateShaderSliderAttribute("Softness", "_Softness", 0f, 3.0f),
            CreateShaderSliderAttribute("Wait", "_Wait", 0f, 10.0f),
            new MaskAttribute()
            {
                Name = "Transition Mask",
                SelectedValue = TransitionMask,
                CallBack = selectedVaue => SetTransition(selectedVaue)
            }
        };

        attributes.AddRange(base.GetAttributes());

        return attributes;
    }

    internal void SetTransition(Texture2D texture)
    {
        TransitionMask = texture.name;
        Material.SetTexture("_TransitionTex", texture);
    }

    internal void SetTransition(string name)
    {
        var masks = MaskImages.AllMasks.FirstOrDefault(x => x.name == name);
        SetTransition(masks);
    }
}

