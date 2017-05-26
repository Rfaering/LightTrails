﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.UI.Models;

public class EffectOption : MonoBehaviour, IPointerClickHandler
{
    private Effect _effect;

    internal void Initialize(Effect effect)
    {
        _effect = effect;
        GetComponentInChildren<Text>().text = effect.Name;

        if (effect.Loop)
        {
            GetComponentInChildren<LoopIcon>().Show();
        }
        else
        {
            GetComponentInChildren<LoopIcon>().Hide();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        FindObjectOfType<ItemsMenu>().AddEffect(_effect);
        FindObjectOfType<EffectOptionsOverlay>().Close();
    }
}
