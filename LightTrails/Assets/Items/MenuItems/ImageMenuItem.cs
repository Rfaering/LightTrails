using Assets.Models;
using Assets.Projects.Scripts;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;

public class ImageMenuItem : MenuItem
{
    public ImageProperties ImageProperties;
    public ShaderAttributes ShaderAttributes;
    public int Index;

    public override Attribute[] GetAttributes()
    {
        var attributes = new List<Attribute>()
        {
            new SizeAttribute()
            {
                Name = "Image Area",
                Resizeable = false,
                ForceWidth = ImageProperties.Width,
                ForceHeight = ImageProperties.Height,
                X = ImageProperties.X,
                Y = ImageProperties.Y,
                SizeChanged = vec => vec,
                OffSetChanged = vec => { ImageProperties.SetPosition(vec); return vec; }
            },
            new ActionAttribute()
            {
                Name = "Change Image",
                Action = () => FindImage()
            },
            new ActionAttribute()
            {
                Name = "Select Shader",
                Action = SelectShader
            },
            new SliderAttribute()
            {
                Name = "Scale",
                CallBack = UpdateScale,
                SelectedValue = ImageProperties.Scale,
                Min = 10
            },
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

    internal void UpdateImageLayer(int index)
    {
        Index = index;
        ImageProperties.SetIndex(index);
        if (ShaderAttributes != null)
        {
            ShaderAttributes.SetIndex(Index);
        }
    }

    internal void Initialize(GameObject image, int index)
    {
        ImageProperties = image.GetComponent<ImageProperties>();
        UpdateImageLayer(index);
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

        ShaderAttributes shaderAttribute;
        if (shaderEffect.MenuItemType != null)
        {
            shaderAttribute = gameObject.AddComponent(shaderEffect.MenuItemType) as ShaderAttributes;
        }
        else
        {
            shaderAttribute = gameObject.AddComponent(typeof(ShaderAttributes)) as ShaderAttributes;
        }

        shaderAttribute.Initialize(newMaterial);
        ShaderAttributes = shaderAttribute;
        ShaderAttributes.SetIndex(Index);

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

    public void UpdateScale(float value)
    {
        ImageProperties.Scale = value;
    }

    public void SelectShader()
    {
        EffectOptionsOverlay.RenderedEffectType = Effect.EffectKind.Shader;
        SceneManager.LoadScene("Scenes/Effects", LoadSceneMode.Additive);
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
        StandaloneFileBrowser.OpenFilePanel(fullPath =>
       {
           SetImage(fullPath);
       });
    }

    public void SetImage(string filePath)
    {
        if (ImageProperties != null && ImageProperties.SetImage(filePath))
        {
            SetNameOfMenuItem(filePath);
        }
    }

    public override void Remove()
    {
        Destroy(ImageProperties.gameObject);
        base.Remove();
    }

    private void SetNameOfMenuItem(string path)
    {
        var fileName = Path.GetFileNameWithoutExtension(path);

        var text = GetComponentInChildren<Text>();
        if (text != null)
        {
            text.text = fileName;
        }
    }

    public override Rect GetRectOfAssociatedItem()
    {
        var result = new Rect(0, 0, ImageProperties.Width, ImageProperties.Height);
        result.center = new Vector2(ImageProperties.X, ImageProperties.Y);
        return result;
    }
}
