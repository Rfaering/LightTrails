using UnityEngine;

public class ImageTranslation : MonoBehaviour
{
    public float X;
    public float Y;

    public void SetTranslation(float x, float y)
    {
        X = x;
        Y = y;
    }

    public void Center()
    {
        SetTranslation(0, 0);
    }

    private void Update()
    {
        transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(X, Y);
    }
}

