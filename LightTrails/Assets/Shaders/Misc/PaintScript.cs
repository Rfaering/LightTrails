using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PaintScript : MonoBehaviour
{
    private Material oldMaterial;

    private Texture2D mask;
    private Material maskMaterial;

    public Texture2D Brush;

    public bool PaintMode = false;

    private Color transparent = new Color(0, 0, 0, 0);

    // Use this for initialization
    void Start()
    {
        mask = new Texture2D(512, 512, TextureFormat.Alpha8, false);
        mask.SetPixels(0, 0, 512, 512, Enumerable.Repeat(transparent, 512 * 512).ToArray());
        maskMaterial = Resources.Load<Material>("Paint");
        maskMaterial.mainTexture = mask;
    }

    public void StartPaintMode()
    {
        var rawImage = gameObject.GetComponent<RawImage>();

        oldMaterial = rawImage.material;
        rawImage.material = maskMaterial;
        PaintMode = true;
    }

    public void StopPaintMode()
    {
        var rawImage = gameObject.GetComponent<RawImage>();
        oldMaterial.SetTexture("_Mask", mask);
        rawImage.material = oldMaterial;
        PaintMode = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PaintMode)
            return;

        if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1))
            return;

        var rect = GetComponent<RectTransform>();
        Vector2 localCursor;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, Input.mousePosition, Camera.main, out localCursor))
            return;

        var percentX = localCursor.x / rect.rect.width;
        var percentY = localCursor.y / rect.rect.height;

        if (!(percentX >= 0 && percentX <= 1.0f && percentY >= 0 && percentY <= 1.0f))
        {
            return;
        }

        RawImage rawImage = GetComponent<RawImage>();

        Texture2D tex = rawImage.material.mainTexture as Texture2D;

        Vector2 startPixel = new Vector2(percentX, percentY);
        startPixel.x *= tex.width;
        startPixel.y *= tex.height;

        startPixel = startPixel - new Vector2(Brush.width / 2, Brush.height / 2);

        for (int i = 0; i < Brush.width; i++)
        {
            for (int j = 0; j < Brush.height; j++)
            {
                Vector2 paintPixel = startPixel + new Vector2(i, j);
                var pixel = tex.GetPixel((int)paintPixel.x, (int)paintPixel.y);

                if (Input.GetMouseButton(0))
                {
                    pixel.a += Brush.GetPixel(i, j).a;
                }
                else
                {
                    pixel.a -= Brush.GetPixel(i, j).a;
                }


                tex.SetPixel((int)paintPixel.x, (int)paintPixel.y, pixel);
            }
        }

        tex.Apply();
    }
}
