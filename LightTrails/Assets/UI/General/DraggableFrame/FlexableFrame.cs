using System;
using UnityEngine;

public class FlexableFrame : MonoBehaviour
{
    public GameObject Frame;
    public Zoom Zoom;
    public bool Resizeable;

    public void Update()
    {
        var sizeItemMenu = FindObjectOfType<SizeMenuItem>();

        if (sizeItemMenu != null && sizeItemMenu.Open)
        {
            Resizeable = sizeItemMenu.SizeAttribute.Resizeable;
            Frame.SetActive(true);
        }
        else
        {
            Frame.SetActive(false);
        }

        foreach (var resizeHandler in GetComponentsInChildren<FlexibleResizeHandler>(true))
        {
            resizeHandler.gameObject.SetActive(Resizeable);
        }
    }

    public void SetOffSet(float x, float y)
    {
        GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
        GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);

        var result = new Vector2(x, y) * Zoom.ZoomFactor;

        GetComponent<RectTransform>().anchoredPosition = result;
    }

    public void SetSize(float width, float height)
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(width, height) * Zoom.ZoomFactor;
    }

    public void OffsetUpdated()
    {
        var pos = GetComponent<RectTransform>().anchoredPosition;
        var sizeMenuItem = FindObjectOfType<SizeMenuItem>();

        if (sizeMenuItem != null)
        {
            var zoomFactor = FindObjectOfType<Zoom>().ZoomFactor;

            var newx = pos.x;
            var newy = pos.y;

            sizeMenuItem.SetOffSet(newx / zoomFactor, newy / zoomFactor);
        }
    }

    public void SizeUpdated()
    {
        var sizeMenuItem = FindObjectOfType<SizeMenuItem>();
        var size = GetComponent<RectTransform>().sizeDelta / Zoom.ZoomFactor;

        if (sizeMenuItem != null)
        {
            sizeMenuItem.SetSize(size.x, size.y);
        }
    }

    internal void ZoomUpdated()
    {
        var sizeMenuItem = FindObjectOfType<SizeMenuItem>();

        if (sizeMenuItem != null)
        {
            var width = sizeMenuItem.SizeAttribute.Width;
            var height = sizeMenuItem.SizeAttribute.Height;

            var x = sizeMenuItem.SizeAttribute.X;
            var y = sizeMenuItem.SizeAttribute.Y;

            GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
            GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);

            GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y) * Zoom.ZoomFactor;
            GetComponent<RectTransform>().sizeDelta = new Vector2(width, height) * Zoom.ZoomFactor;
        }
    }
}
