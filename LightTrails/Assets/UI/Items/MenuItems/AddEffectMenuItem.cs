using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AddEffectMenuItem : MonoBehaviour, IPointerClickHandler
{
    private Record recorder;

    private bool IsDisabled { get { return recorder.Recording; } }

    public Color DisabledColor = new Color(0.8f, 0.8f, 0.8f);
    public Color NormalColor = new Color(1.0f, 1.0f, 1.0f);

    public Attribute[] Attributes;

    private void Start()
    {
        recorder = FindObjectOfType<Record>();
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (IsDisabled)
        {
            return;
        }

        var overlay = Resources.FindObjectsOfTypeAll<EffectOptionsOverlay>().First();
        overlay.Open();
    }

    void Update()
    {
        if (IsDisabled)
        {
            GetComponent<Image>().color = DisabledColor;
        }
        else
        {
            GetComponent<Image>().color = NormalColor;
        }
    }
}
