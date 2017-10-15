using UnityEngine;

public class CameraScaling : MonoBehaviour
{
    public bool DebugMode = false;

    public Zoom Zoom;

    public void Start()
    {
        Zoom = FindObjectOfType<Zoom>();
    }

    // Update is called once per frame
    void Update()
    {
#if DEBUG
        if (Input.GetKeyDown(KeyCode.D))
        {
            DebugMode = !DebugMode;
        }
#endif
        var camera = GetComponent<Camera>();

        if (DebugMode)
        {
            //FindObjectOfType<FlexableFrame>().GetComponentInChildren<Image>
            var imagePicker = FindObjectOfType<RecorderAreaPicker>();

            int width = imagePicker.Width;
            int height = imagePicker.Height;

            float widthRatio = camera.pixelWidth / (float)width;
            float heightRatio = camera.pixelHeight / (float)height;

            if (widthRatio > heightRatio)
            {
                camera.orthographicSize = camera.pixelHeight / 200.0f / heightRatio;
            }
            else
            {
                camera.orthographicSize = camera.pixelHeight / 200.0f / widthRatio;
            }

            camera.transform.position = new Vector3((imagePicker.X / 100.0f), (imagePicker.Y / 100.0f), 0);
        }
        else
        {
            camera.transform.position = new Vector3(0, 0, 0);
            camera.orthographicSize = (camera.pixelHeight / 200.0f) / Zoom.ZoomFactor;
        }
    }
}
