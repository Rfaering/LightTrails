using Assets.UI.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EffectContentContainer : MonoBehaviour
{
    public GameObject Prefab;

    public Effect.EffectKind EffectKind = Effect.EffectKind.Particle;

    void Start()
    {
        foreach (var menuItem in GetComponentsInChildren<EffectOption>())
        {
            DestroyObject(menuItem.gameObject);
        }

        foreach (var effect in EffectOptions.Options.Where(x => x.Type == EffectKind))
        {
            var newGameObject = Instantiate(Prefab, transform);
            newGameObject.name = effect.Name;
            newGameObject.GetComponent<EffectOption>().Initialize(effect);
        }
    }
}
