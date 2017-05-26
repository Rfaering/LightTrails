using UnityEngine;
using UnityEngine.UI;

public class ImageAreaPicker : MonoBehaviour
{
    private Texture2D backgroundTexture;
    private GUIStyle textureStyle;

    public void DrawCenteredRedRect(float width, float height)
    {
        DrawRect(new Rect((Camera.main.pixelWidth / 2) - width / 2, (Camera.main.pixelHeight / 2) - height / 2, width, height), new Color(1.0f, 0.0f, 0.0f, 0.5f));
    }

    public void DrawRedCenter()
    {
        DrawRect(Center(514), Color.red);
    }

    internal Rect GetRect()
    {
        var rawImage = GetComponent<RawImage>();
        var width = rawImage.texture.width;
        var height = rawImage.texture.height;
        return new Rect((Camera.main.pixelWidth / 2) - width/2, (Camera.main.pixelHeight / 2) - height / 2, width, height);
    }

    internal Rect Center(int size)
    {
        var width = size;
        var height = size;
        return new Rect((Camera.main.pixelWidth / 2) - width / 2, (Camera.main.pixelHeight / 2) - height / 2, width, height);
    }

    public void DrawRect(Rect position, Color color, GUIContent content = null)
    {
        var backgroundColor = GUI.backgroundColor;
        GUI.backgroundColor = color;
        GUI.Box(position, content ?? GUIContent.none, textureStyle);
        GUI.backgroundColor = backgroundColor;
    }

    // Use this for initialization
    void Start()
    {
        backgroundTexture = Texture2D.whiteTexture;

        textureStyle = new GUIStyle { normal = new GUIStyleState { background = backgroundTexture } };
    }
}


