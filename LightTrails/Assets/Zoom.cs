using UnityEngine;
using UnityEngine.UI;

public class Zoom : MonoBehaviour
{
    public float ZoomFactor;

    void Start()
    {
        var slider = GetComponent<Slider>();
        slider.wholeNumbers = true;
        slider.minValue = 1;
        slider.maxValue = 5;
        slider.value = 4;
        slider.onValueChanged.AddListener(HandleChange);

        ZoomFactor = 1.0f;
    }

    private void HandleChange(float arg0)
    {
        ZoomFactor = arg0 / 4.0f;

        var text = GetComponentInChildren<Text>();
        text.text = ZoomFactor * 100 + " %";

        FindObjectOfType<FlexableFrame>().ZoomUpdated();
    }
}
