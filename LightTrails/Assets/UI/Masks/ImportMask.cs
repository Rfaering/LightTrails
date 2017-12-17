using Assets.Projects.Scripts;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ImportMask : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        StandaloneFileBrowser.OpenFilePanel(callBack =>
        {
            var maskList = FindObjectOfType<MaskList>();

            var texture = GetTexture(callBack);
            if (texture != null)
            {
                maskList.AddMask(texture);
            }
        });
    }

    private Texture2D GetTexture(string path)
    {
        if (File.Exists(path))
        {
            try
            {
                Texture2D tex = new Texture2D(1, 1);
                var bytes = File.ReadAllBytes(path);
                tex.LoadImage(bytes);

                if (Project.CurrentModel != null)
                {
                    Project.CurrentModel.SaveMask(bytes);
                }

                return tex;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        return null;
    }
}
