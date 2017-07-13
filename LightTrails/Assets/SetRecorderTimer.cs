using UnityEngine;
using UnityEngine.UI;

public class SetRecorderTimer : MonoBehaviour
{
    private Record _record;

    private void Start()
    {
        _record = FindObjectOfType<Record>();
        GetComponent<Slider>().onValueChanged.AddListener(ValueChanged);
    }

    private void ValueChanged(float percentage)
    {
        _record.SetPercentage(percentage);
    }
}
