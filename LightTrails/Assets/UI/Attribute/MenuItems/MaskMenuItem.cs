using Assets.Models;
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

        var maskList = Resources.FindObjectsOfTypeAll<MaskList>().FirstOrDefault();
        maskList.EnsureLoaded();

        _maskAttribute = maskAttribute;
        GetComponentInChildren<Button>().onClick.AddListener(OpenMaskPanel);

        var maskItems = Resources.FindObjectsOfTypeAll<MaskItem>();
        var maskItem = maskItems.FirstOrDefault(x => x.Name == _maskAttribute.SelectedValue);
        maskItem.Select();

        EnsureImageIsCorrect();
    }

    public void SetSelection(string selection)
    {
        _maskAttribute.SelectedValue = selection;
        EnsureImageIsCorrect();
        _maskAttribute.Changed(selection);
    }

    private void EnsureImageIsCorrect()
    {
        var mask = MaskImages.AllMasks.FirstOrDefault(x => x.name == _maskAttribute.SelectedValue);
        if (mask != null)
        {
            var childComponent = GetComponentsInChildren<RawImage>().Last();
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
