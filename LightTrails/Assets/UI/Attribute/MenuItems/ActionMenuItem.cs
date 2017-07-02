using Assets.Models;
using UnityEngine.UI;

public class ActionMenuItem : AttributeMenuItem
{
    private ActionAttribute _actionAttribute;

    internal void Initialize(ActionAttribute actionAttribute)
    {
        GetComponentInChildren<Button>().onClick.AddListener(() => actionAttribute.Action());
        GetComponentInChildren<Text>().text = actionAttribute.Name;
        GetComponentInChildren<Button>().interactable = actionAttribute.IsEnabled();
        _actionAttribute = actionAttribute;
    }

    public override void ReEvaluateEnabled()
    {
        var button = GetComponentInChildren<Button>();

        if (_actionAttribute == null || button == null)
        {
            return;
        }

        button.interactable = _actionAttribute.IsEnabled();
    }
}
