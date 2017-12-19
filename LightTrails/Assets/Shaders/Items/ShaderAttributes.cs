using Assets.Models;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShaderAttributes : MonoBehaviour
{
    public Material Material;

    public string SelectedMaskValue = "Mask01";

    public ShaderSliderAttribute CreateShaderSliderAttribute(string displayName, string propertyName, float min, float max)
    {
        return new ShaderSliderAttribute(displayName, propertyName, min, max, Material);
    }

    public void Initialize(Material material)
    {
        Material = material;

        if (material != null)
        {
            material.renderQueue = (int)gameObject.transform.localPosition.z;
        }
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

    internal void SetIndex(int index)
    {
        Material.renderQueue = 3000 - 100 * index;
    }
}
