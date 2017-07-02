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
        EffectName = effect.Name;
        GetComponentInChildren<Text>().text = effect.Name;
        var gameObject = GetShaderEffect();
        gameObject.SetActive(true);
        assosicatedEffect = gameObject;

        Material = assosicatedEffect.GetComponent<RawImage>().material;
    }


    public override List<Attribute> GetAttributes()
    {
        var attributes = base.GetAttributes();
        var texture = Material.GetTexture("_Mask");
        if (texture != null)
        {
            attributes.Add(new MaskAttribute() { SelectedValue = "Mask01" });
        }

        return attributes;
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
