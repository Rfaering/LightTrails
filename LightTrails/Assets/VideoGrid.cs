using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VideoGrid : MonoBehaviour, IPointerClickHandler
{
    public void Open()
    {
        gameObject.SetActive(true);
        GetComponentInChildren<VideoContainer>().Initialize();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Close();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
