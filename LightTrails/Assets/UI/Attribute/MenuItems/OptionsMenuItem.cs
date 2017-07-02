using Assets.Models;
using UnityEngine.UI;

public class OptionsMenuItem : AttributeMenuItem
{
    private OptionsAttribute _options;

    internal void Initialize(OptionsAttribute options)
    {
        _options = options;
        GetComponentInChildren<Text>().text = options.Name;
        Dropdown dropdown = GetComponentInChildren<Dropdown>();
        dropdown.ClearOptions();
        dropdown.AddOptions(options.Options);
        var defaultSelectedOption = dropdown.options.Find(x => x.text == options.SelectedValue);
        dropdown.value = dropdown.options.IndexOf(defaultSelectedOption);
        dropdown.onValueChanged.AddListener(index => options.Changed(dropdown.options[index].text));
    }

    public override void ReEvaluateEnabled()
    {
        var dropDown = GetComponentInChildren<Dropdown>();

        if (_options == null || dropDown == null)
        {
            return;
        }

        dropDown.interactable = _options.IsEnabled();
    }
}