using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class VideoGrid : MonoBehaviour, IPointerClickHandler
{
    public void Start()
    {
        GetComponentInChildren<VideoContainer>().Initialize();
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Videos"));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Close();
    }

    public void Close()
    {
        SceneManager.UnloadSceneAsync("Scenes/Videos");
    }
}
