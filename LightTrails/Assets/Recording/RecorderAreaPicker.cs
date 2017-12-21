using UnityEngine;

public class RecorderAreaPicker : MonoBehaviour
{
    public int X = 0;
    public int Y = 0;
    public int Width = 800;
    public int Height = 600;

    public Vector2 SetOffSet(Vector2 offSet)
    {
        X = (int)offSet.x;
        Y = (int)offSet.y;

        FindObjectOfType<FlexableFrame>().SetOffSet(offSet.x, offSet.y);

        return offSet;
    }

    public Vector2 SetSize(Vector2 size)
    {
        Width = (int)size.x;
        Height = (int)size.y;

        return size;
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


