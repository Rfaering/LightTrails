using Assets.Models;
using Assets.Projects.Scripts;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Collections.Generic;

public class ImageMenuItem : MenuItem
{
    public ImageProperties ImageProperties;
    public ShaderAttributes ShaderAttributes;

    public override Attribute[] GetAttributes()
    {
        var attributes = new List<Attribute>()
        {
            new ActionAttribute()
            {
                Name = "Change Image",
                Action = () => FindImage()
            },
            new SliderAttribute()
            {
                Name = "Scale",
                CallBack = UpdateScale,
                SelectedValue = ImageProperties.Scale,
                Min = 10
            },
            new ActionAttribute()
            {
                Name = "Select Shader",
                Action = SelectShader
            }
            /*new ToggleAttribute()
            {
                Name = "Light",
                CallBack = value => FindObjectOfType<LightPlane>().SetEnabled(value)
            }*/
        };

        var shaderAttributes = ShaderAttributes;
        if (shaderAttributes != null)
        {
            attributes.AddRange(shaderAttributes.GetAttributes());
        }

        return attributes.ToArray();

        /*if (Project.CurrentModel != null)
        {
            SetSaveState(Project.CurrentModel.Items.Image);
        }*/
    }

    internal void Initialize(GameObject image)
    {
        ImageProperties = image.GetComponent<ImageProperties>();
    }

    public void SetShader(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return;
        }

        var shader = EffectOptions.Options.Where(x => x.Name == name && x.Type == Effect.EffectKind.Shader).FirstOrDefault();
        SetShader(shader);
    }

    public void SetShader(Effect shaderEffect)
    {
        if (shaderEffect == null)
        {
            return;
        }

        var material = GetShaderEffect(shaderEffect.Name);

        var newMaterial = new Material(material);

        ImageProperties.GetComponent<RawImage>().material = newMaterial;

        var shaderComponent = gameObject.GetComponent<ShaderAttributes>();
        if (shaderComponent != null)
        {
            Destroy(shaderComponent);
            ShaderAttributes = null;
        }

        if (shaderEffect.MenuItemType != null)
        {
            var newComponent = gameObject.AddComponent(shaderEffect.MenuItemType) as ShaderAttributes;
            newComponent.Initialize(newMaterial);
            ShaderAttributes = newComponent;
        }

        FindObjectOfType<AttributesMenu>().CreateProperties(GetAttributes());
    }

    private Material GetShaderEffect(string name)
    {
        var materials = Resources.LoadAll<Material>("Materials");
        var dictionary = materials.ToDictionary(x => x.name);
        if (dictionary.ContainsKey(name))
        {
            return dictionary[name];
        }

        return null;
    }

    public void UpdateLighting(float value)
    {
        ImageProperties.Lighting = value;
    }

    public void UpdateTransparency(float value)
    {
        ImageProperties.Transparency = value;
    }

    public void UpdateScale(float value)
    {
        ImageProperties.Scale = value;
    }

    public void SelectShader()
    {
        var overlay = Resources.FindObjectsOfTypeAll<EffectOptionsOverlay>().First();
        overlay.Open(Effect.EffectKind.Shader);
    }

    internal StoredImageItem GetImageSaveState()
    {
        return new StoredImageItem()
        {
            ImagePath = ImageProperties.ImagePath,
            Shader = ShaderAttributes != null ? ShaderAttributes.Material.name : string.Empty,
            Attributes = GetSaveState().Attributes,
            Index = transform.GetSiblingIndex()
        };
    }

    public void FindImage()
    {
        StandaloneFileBrowser.OpenFilePanel(callBack =>
       {
           FindObjectOfType<ImageProperties>().SetImage(callBack);
       });
    }

    public override void Remove()
    {
        Destroy(ImageProperties.gameObject);
        base.Remove();
    }
}
