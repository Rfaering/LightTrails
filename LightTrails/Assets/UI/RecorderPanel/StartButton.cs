using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    private Record _recorder;

    public Texture2D PlayTexture;
    public Texture2D PauseTexture;

    public Color PlayColor;
    public Color PauseColor;

    void Start()
    {
        _recorder = FindObjectOfType<Record>();
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        _recorder.Playing = !_recorder.Playing;
    }

    void Update()
    {
        var isRunning = _recorder.Playing;

        RawImage rawImage = GetComponentInChildren<RawImage>();
        var rect = rawImage.gameObject.GetComponent<RectTransform>();
        if (isRunning)
        {
            rawImage.color = PauseColor;
            rawImage.texture = PauseTexture;

            rect.offsetMin = new Vector2(25, 25);
            rect.offsetMax = new Vector2(-25, -25);
        }
        else
        {
            rawImage.color = PlayColor;
            rawImage.texture = PlayTexture;

            rect.offsetMin = new Vector2(25, 20);
            rect.offsetMax = new Vector2(-15, -20);
        }
    }
}
