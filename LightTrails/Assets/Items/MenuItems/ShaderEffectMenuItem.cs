using Assets.UI.Models;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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

    private GameObject GetShaderEffect()
    {
        var dictionary = Resources.FindObjectsOfTypeAll<ShaderEffect>().ToDictionary(x => x.gameObject.name);
        if (dictionary.ContainsKey(EffectName))
        {
            return dictionary[EffectName].gameObject;
        }

        return null;
    }

    public override void Remove()
    {
        GetShaderEffect().SetActive(false);
        base.Remove();
    }
}
