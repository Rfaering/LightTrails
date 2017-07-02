using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
using System;

public class MaskPanel : MonoBehaviour, IPointerClickHandler
{
    public static void EnsureLoaded()
    {
        var panel = Resources.FindObjectsOfTypeAll<MaskPanel>().FirstOrDefault();
        if (!panel.gameObject.activeInHierarchy)
        {
            panel.gameObject.SetActive(true);
        }
    }

    public void Start()
    {
        gameObject.SetActive(false);
    }

    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }

    public static void Close()
    {
        var panel = Resources.FindObjectsOfTypeAll<MaskPanel>().FirstOrDefault();
        panel.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Close();
    }
}
