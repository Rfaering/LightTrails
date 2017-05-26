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
        var dialog = GetComponentInParent<NewProjectDialog>();

        // Open file with filter
        var extensions = new[] {
            new ExtensionFilter("Image Files", "png", "jpg", "jpeg" ),
        };
        var paths = StandaloneFileBrowser.OpenFilePanel("Open File", System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyPictures), extensions, true);

        if (paths != null && paths.Length > 0)
        {
            var path = paths.First();

            try
            {
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
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }

    internal void SetDefaultImage()
    {
        GetComponent<RawImage>().texture = DefaultImage;
        GetComponent<RawImage>().SizeToParent();
    }
}
