using UnityEngine;
using UnityEngine.UI;

public class RecordButton : MonoBehaviour
{
    private Record _recorder;

    public Texture2D RecordTexture;
    public Texture2D StopTexture;

    public Color RecordButtonColor;
    public Color StopButtonColor;

    public Color RecordFillColor;
    public Color NotRecordFillColor;

    public GameObject FillGameObject;

    void Start()
    {
        _recorder = FindObjectOfType<Record>();
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (_recorder.ActivelyRecording)
        {
            _recorder.StopRecording();
        }
        else
        {
            _recorder.StartRecording();
        }
    }

    // Update is called once per frame
    void Update()
    {
        var isRecording = _recorder.ActivelyRecording;
        Image image = FillGameObject.GetComponent<Image>();

        RawImage rawImage = GetComponentInChildren<RawImage>();
        var rect = rawImage.gameObject.GetComponent<RectTransform>();

        if (isRecording)
        {
            image.color = RecordFillColor;

            rawImage.color = StopButtonColor;
            rawImage.texture = StopTexture;

            rect.offsetMin = new Vector2(15, 15);
            rect.offsetMax = new Vector2(-15, -15);
        }
        else
        {
            image.color = NotRecordFillColor;

            rawImage.color = RecordButtonColor;
            rawImage.texture = RecordTexture;

            rect.offsetMin = new Vector2(5, 5);
            rect.offsetMax = new Vector2(-5, -5);
        }
    }
}
