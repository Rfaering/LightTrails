using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AddEffectMenuItem : MonoBehaviour, IPointerClickHandler
{
    private Record recorder;

    private bool IsDisabled { get { return recorder.ActivelyRecording; } }

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
        GetComponent<Button>().interactable = !IsDisabled;
    }
}
