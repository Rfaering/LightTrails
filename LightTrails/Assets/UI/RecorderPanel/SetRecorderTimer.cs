using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SetRecorderTimer : MonoBehaviour
{
    private Record _record;
    private RecorderProgressBar _recorderProgressBar;

    private void Start()
    {
        _recorderProgressBar = GetComponentInChildren<RecorderProgressBar>();
        _record = FindObjectOfType<Record>();
        GetComponentInChildren<Slider>().value = 0;
        GetComponentInChildren<Slider>().onValueChanged.AddListener(ValueChanged);
    }

    private void Update()
    {
        /*var particleMenuItemsCount = FindObjectsOfType<ParticleEffectMenuItem>().Count();
        if (particleMenuItemsCount > 0 || _record.ActivelyRecording)
        {
            _recorderProgressBar.gameObject.SetActive(true);
        }
        else
        {
            _recorderProgressBar.gameObject.SetActive(false);
        }*/
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
        _recorderProgressBar.GetComponent<Slider>().value = value;
    }

    private void ValueChanged(float percentage)
    {
        _record.SetPercentage(percentage);
    }
}
