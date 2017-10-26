using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseVideoGrid : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GetComponentInParent<VideoGrid>().Close();
        }
    }

    private void OnClick()
    {
        GetComponentInParent<VideoGrid>().Close();
    }
}
