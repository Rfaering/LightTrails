using UnityEngine;
using UnityEngine.UI;

public class SetRecorderTimer : MonoBehaviour
{
    private Record _record;

    private void Start()
    {
        _record = FindObjectOfType<Record>();
        GetComponentInChildren<Slider>().value = 0;
        GetComponentInChildren<Slider>().onValueChanged.AddListener(ValueChanged);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetProgress(float value)
    {
        GetComponentInChildren<Slider>().value = value;
    }

    private void ValueChanged(float percentage)
    {
        _record.SetPercentage(percentage);
    }
}
