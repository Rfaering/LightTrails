using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectContentContainer : MonoBehaviour
{
    public GameObject Prefab;

    void Start()
    {
        foreach (var menuItem in GetComponentsInChildren<EffectOption>())
        {
            DestroyObject(menuItem.gameObject);
        }

        foreach (var effect in EffectOptions.Options)
        {
            var newGameObject = Instantiate(Prefab, transform);
            newGameObject.name = effect.Name;
            newGameObject.GetComponent<EffectOption>().Initialize(effect);
        }
    }
}
