﻿using Assets.Models;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MaskMenuItem : AttributeMenuItem
{
    private MaskAttribute _maskAttribute;
    private MaskPanel _maskPanel;

    public Color Open;
    public Color Closed;

    internal void Initialize(MaskAttribute maskAttribute)
    {
        _maskPanel = Resources.FindObjectsOfTypeAll<MaskPanel>().FirstOrDefault();
        _maskAttribute = maskAttribute;
        GetComponentInChildren<Button>().onClick.AddListener(OpenMaskPanel);

        var maskItem = Resources.FindObjectsOfTypeAll<MaskItem>().FirstOrDefault(x => x.Name == _maskAttribute.SelectedValue);
        maskItem.Select();

        EnsureImageIsCorrect();
    }

    public void SetSelection(string selection)
    {
        _maskAttribute.SelectedValue = selection;
        EnsureImageIsCorrect();
    }

    private void EnsureImageIsCorrect()
    {
        var mask = MaskImages.Masks.FirstOrDefault(x => x.name == _maskAttribute.SelectedValue);
        if (mask != null)
        {
            var childComponent = GetComponentInChildren<RawImage>();
            childComponent.texture = mask;
        }
    }

    public void OpenMaskPanel()
    {
        _maskPanel.Toggle();
    }

    private void Update()
    {
        if (_maskPanel != null && _maskPanel.gameObject.activeInHierarchy)
        {
            GetComponent<Image>().color = Open;
        }
        else
        {
            GetComponent<Image>().color = Closed;
        }
    }

    public override void ReEvaluateEnabled()
    {
        var button = GetComponentInChildren<Button>();

        if (_maskAttribute == null || button == null)
        {
            return;
        }

        button.interactable = _maskAttribute.IsEnabled();
    }
}