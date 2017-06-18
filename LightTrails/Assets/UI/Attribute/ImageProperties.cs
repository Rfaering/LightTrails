using Assets.Projects.Scripts;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ImageProperties : MonoBehaviour
{
    [Range(10, 100)]
    public float Lighting = 100;

    [Range(10, 100)]
    public float Transparency = 100;

    [Range(10, 100)]
    public float Scale = 100;

    public float CorrectionScale = 1.0f;

    // Use this for initialization
    void Start()
    {
        if (Project.CurrentModel != null)
        {
            SetImage(Project.CurrentModel.GetLocalImagePath());
        }

    }

    // Update is called once per frame
    void Update()
    {
        var color = Lighting / 100f;
        var alpha = Transparency / 100f;
        GetComponent<RawImage>().color = new Color(color, color, color, alpha);
        transform.localScale = CorrectionScale * new Vector3(Scale / 100, Scale / 100, Scale / 100);
    }

    public void SetImage(string path)
    {
        if (File.Exists(path))
        {
            try
            {
                Texture2D tex = new Texture2D(1, 1);
                var bytes = File.ReadAllBytes(path);
                tex.LoadImage(bytes);
                var rawImage = GetComponent<RawImage>();
                rawImage.texture = tex;
                rawImage.rectTransform.sizeDelta = new Vector2(tex.width, tex.height);

                GetComponent<ImageAreaPicker>().Recalculate();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}

