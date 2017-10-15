using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.Models;

public class EffectOption : MonoBehaviour, IPointerClickHandler
{
    private Effect _effect;

    internal void Initialize(Effect effect)
    {
        _effect = effect;
        GetComponentInChildren<Text>().text = effect.Name;
        GetComponentInChildren<EffectOptionImageLoader>().SetImage(effect);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        FindObjectOfType<ItemsMenu>().AddEffect(_effect);
        FindObjectOfType<EffectOptionsOverlay>().Close();
    }
}
