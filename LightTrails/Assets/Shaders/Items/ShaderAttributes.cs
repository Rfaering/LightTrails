﻿using Assets.Models;
using Assets.Projects.Scripts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ShaderAttributes : MonoBehaviour
{
    public Material Material;

    public string SelectedMaskValue = "Mask01";

    public void Initialize(Material material)
    {
        Material = material;
    }

    internal void SetMask(Texture2D texture)
    {
        SelectedMaskValue = texture.name;
        Material.SetTexture("_AttMask", texture);
    }

    internal void SetMask(string name)
    {
        var masks = MaskImages.AllMasks.FirstOrDefault(x => x.name == name);
        SetMask(masks);
    }

    public virtual List<Attribute> GetAttributes()
    {
        var attributes = new List<Attribute>();
        var hasTexture = Material.HasProperty("_AttMask");

        if (hasTexture)
        {
            attributes.Add(new MaskAttribute()
            {
                SelectedValue = SelectedMaskValue,
                CallBack = selectedVaue => SetMask(selectedVaue)
            });
        }

        return attributes;
    }
}
