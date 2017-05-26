using ProgressBar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoProgressBar : MonoBehaviour
{

    private void Start()
    {
        GetComponent<ProgressBarBehaviour>().Value = 0;
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
        GetComponent<ProgressBarBehaviour>().Value = value * 100;
    }
}
