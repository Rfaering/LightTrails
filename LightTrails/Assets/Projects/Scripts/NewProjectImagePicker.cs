using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NewProjectImagePicker : MonoBehaviour
{
    public string selectedPath;

    public Texture DefaultImage;

    // Use this for initialization
    void Start()
    {
        GetComponentInChildren<Button>().onClick.AddListener(OpenDialog);

    }

    private void OpenDialog()
    {
        StandaloneFileBrowser.OpenFilePanel(SetBasedOnPath);
    }

    internal void SetBasedOnPath(string path)
    {
        var dialog = GetComponentInParent<NewProjectDialog>();

        Texture2D tex = new Texture2D(0, 0);
        var bytes = File.ReadAllBytes(path);
        tex.LoadImage(bytes);

        GetComponent<RawImage>().texture = tex;
        GetComponent<RawImage>().SizeToParent();

        var projectName = dialog.GetProjectName();

        if (string.IsNullOrEmpty(projectName))
        {
            var name = Path.GetFileNameWithoutExtension(path);
            dialog.SetProjectName(name);
        }

        selectedPath = path;
    }

    internal void SetDefaultImage()
    {
        GetComponent<RawImage>().texture = DefaultImage;
        GetComponent<RawImage>().SizeToParent();
    }
}
