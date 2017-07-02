using Assets.Models;
using UnityEngine.UI;

public class SliderMenuItem : AttributeMenuItem
{
    private SliderAttribute _slider;

    internal void Initialize(SliderAttribute slider)
    {
        _slider = slider;
        GetComponentInChildren<Text>().text = slider.Name;
        var sliderComponent = GetComponentInChildren<Slider>();
        sliderComponent.minValue = slider.Min;
        sliderComponent.maxValue = slider.Max;
        sliderComponent.value = slider.SelectedValue;
        sliderComponent.onValueChanged.AddListener(value => slider.Changed(value));
    }

    public override void ReEvaluateEnabled()
    {
        var slider = GetComponentInChildren<Slider>();

        if (_slider == null || slider == null)
        {
            return;
        }

        slider.interactable = _slider.IsEnabled();
    }
}
