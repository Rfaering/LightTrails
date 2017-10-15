using Assets.Models;
using UnityEngine;
using UnityEngine.EventSystems;

public class EffectOptionsOverlay : MonoBehaviour, IPointerClickHandler
{
    internal void Close()
    {
        foreach (var item in GetComponentsInChildren<EffectContentContainer>(true))
        {
            item.gameObject.transform.parent.gameObject.SetActive(true);
        }

        FindObjectOfType<Record>().ShowProgressBar = true;
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FindObjectOfType<EffectOptionsOverlay>().Close();
        }
    }

    internal void Open(Effect.EffectKind effectType)
    {
        foreach (var item in GetComponentsInChildren<EffectContentContainer>())
        {
            item.gameObject.transform.parent.gameObject.SetActive(item.EffectKind == effectType);
        }

        FindObjectOfType<Record>().ShowProgressBar = false;
        MaskPanel.Close();
        gameObject.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        FindObjectOfType<EffectOptionsOverlay>().Close();
    }
}
