using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Assets.Models;
using System.Collections.Generic;

public class ShaderEffectMenuItem : EffectMenuItem
{
    public Material Material;

    public override void Initialize(Effect effect)
    {
        var gameObject = GetShaderEffect();
        gameObject.SetActive(true);

        assosicatedEffect = gameObject;
        //Material = assosicatedEffect.GetComponent<RawImage>().material;

        var record = FindObjectOfType<Record>();
        assosicatedEffect.GetComponent<RunningEffect>().Initialize(record.RecordingTime);
    }


    public override Attribute[] GetAttributes()
    {
        var attributes = base.GetAttributes().ToList();
        var hasTexture = Material.HasProperty("_AttMask");
        if (hasTexture)
        {
            attributes.Add(new MaskAttribute() { Name = "Mask", SelectedValue = "Mask01" });
        }

        return attributes.ToArray();
    }

    private GameObject GetShaderEffect()
    {
        var dictionary = Resources.FindObjectsOfTypeAll<ShaderEffect>().ToDictionary(x => x.gameObject.name);
        if (dictionary.ContainsKey(EffectName))
        {
            return dictionary[EffectName].gameObject;
        }

        return null;
    }

    internal void SetMask(Texture2D texture)
    {
        assosicatedEffect.GetComponent<RawImage>().material.SetTexture("_AttMask", texture);
    }

    public override void Remove()
    {
        GetShaderEffect().SetActive(false);
        base.Remove();
    }
}
