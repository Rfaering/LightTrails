using UnityEngine;
using UnityEngine.UI;

static class CanvasExtensions
{
    public static Vector2 SizeToParent(this RawImage image)
    {
        var parent = image.transform.parent.GetComponent<RectTransform>();
        return SizeToBounds(image, parent.rect.width, parent.rect.height);
    }

    public static Vector2 SizeToBounds(this RawImage image, float width, float height)
    {        
        var imageTransform = image.GetComponent<RectTransform>();
        float w = 0, h = 0;
        float ratio = image.texture.width / (float)image.texture.height;
        var bounds = new Rect(0, 0, width, height);

        //Size by height first
        h = bounds.height;
        w = h * ratio;
        if (w > bounds.width)
        { //If it doesn't fit, fallback to width;
            w = bounds.width;
            h = w / ratio;
        }
        imageTransform.sizeDelta = new Vector2(w, h);
        return imageTransform.sizeDelta;
    }
}