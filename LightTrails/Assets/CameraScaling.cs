using UnityEngine;

public class CameraScaling : MonoBehaviour
{
    public void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var imageAreaPicker = FindObjectOfType<ImageAreaPicker>();

        float ch = Camera.main.pixelHeight;
        float h = imageAreaPicker.PixelHeight;

        // Assumming pixel per unit is 100
        Camera.main.orthographicSize = ch * (h / 100.0f) / (2.0f * h);
    }
}
