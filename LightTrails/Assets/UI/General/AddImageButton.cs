using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddImageButton : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(HandleClick);
    }

    private void HandleClick()
    {
        StandaloneFileBrowser.OpenFilePanel(callBack =>
        {
            var itemsmenu = FindObjectOfType<ItemsMenu>();
            var image = itemsmenu.AddImage();
            image.SetImage(callBack);
            image.SetShader("Brightness");
            itemsmenu.ItemSelected(image);
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
