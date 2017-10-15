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

    public string ImagePath;

    // Use this for initialization
    void Start()
    {

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
        ImagePath = path;

        if (File.Exists(path))
        {
            try
            {
                Texture2D tex = new Texture2D(1, 1);
                tex.filterMode = FilterMode.Bilinear;
                tex.wrapMode = TextureWrapMode.Clamp;

                var bytes = File.ReadAllBytes(path);
                tex.LoadImage(bytes);
                var rawImage = GetComponent<RawImage>();
                rawImage.texture = tex;
                rawImage.rectTransform.sizeDelta = new Vector2(tex.width, tex.height);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}

