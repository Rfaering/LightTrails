using UnityEngine;

public class RecorderAreaPicker : MonoBehaviour
{
    public int X = 0;
    public int Y = 0;
    public int Width = 800;
    public int Height = 600;

    public void SetOffSet(float x, float y)
    {
        X = (int)x;
        Y = (int)y;

        FindObjectOfType<FlexableFrame>().SetOffSet(x, y);
    }

    public void SetSize(float width, float height)
    {
        Width = (int)width;
        Height = (int)height;

        FindObjectOfType<FlexableFrame>().SetSize(width, height);
    }

    internal Rect GetRect()
    {
        var width = Width;
        var height = Height;
        return new Rect(((Camera.main.pixelWidth / 2) - width / 2) + X, ((Camera.main.pixelHeight / 2) - height / 2) + Y, width, height);
    }

    internal Rect Center(int size)
    {
        var width = size;
        var height = size;
        return new Rect((Camera.main.pixelWidth / 2) - width / 2, (Camera.main.pixelHeight / 2) - height / 2, width, height);
    }

    // Use this for initialization
    void Start()
    {

    }
}


