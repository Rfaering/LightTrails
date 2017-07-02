using Assets.Models;
using UnityEngine.UI;

public class ToggleMenuItem : AttributeMenuItem
{
    private ToggleAttribute _toggle;

    internal void Initialize(ToggleAttribute toggle)
    {
        _toggle = toggle;
        GetComponentInChildren<Text>().text = toggle.Name;
        var toggleComponent = GetComponentInChildren<Toggle>();
        toggleComponent.isOn = toggle.SelectedValue;
        toggleComponent.onValueChanged.AddListener(value => toggle.Changed(value));
    }

    public override void ReEvaluateEnabled()
    {
        var toggle = GetComponentInChildren<Toggle>();

        if (_toggle == null || toggle == null)
        {
            return;
        }

        toggle.interactable = _toggle.IsEnabled();
    }
}
