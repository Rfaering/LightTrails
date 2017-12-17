using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ImageProperties : MonoBehaviour
{
    [Range(10, 100)]
    public float Scale = 100;

    public float CorrectionScale = 1.0f;

    public float Width;
    public float Height;

    public float X;
    public float Y;

    public string ImagePath;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = CorrectionScale * new Vector3(Scale / 100, Scale / 100, Scale / 100);

        var rect = GetComponent<RectTransform>();
        var deltaSize = rect.sizeDelta;

        Width = deltaSize.x;
        Height = deltaSize.y;

        X = rect.anchoredPosition.x;
        Y = rect.anchoredPosition.y;
    }

    public void SetPosition(Vector2 position)
    {
        var rect = GetComponent<RectTransform>();
        rect.anchoredPosition = position;
    }

    public bool SetImage(string path)
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
                Width = tex.width;
                Height = tex.height;

                return true;
            }
            catch (Exception e)
            {
                Debug.LogException(e);

            }
        }

        return false;
    }

    internal void SetIndex(int index)
    {
        var zIndex = 3000 - index * 100;

        var oldPosition = gameObject.transform.localPosition;
        gameObject.transform.localPosition = new Vector3(oldPosition.x, oldPosition.y, zIndex);
    }
}

