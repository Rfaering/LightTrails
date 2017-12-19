using FfmpegWrapper;
using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.SceneManagement;

public partial class Record : MonoBehaviour
{
    public bool ActivelyRecording = false;
    public bool Playing = false;
    public bool ShowProgressBar = true;

    public float TimeBetweenFrames = 1.0f / 25.0f;

    public float ElapsedVideoTime = 0;
    public float RecordingTime = 10;

    public GameObject ProgressBar;
    private SetRecorderTimer _recordTimer;

    public int Frame = 0;

    void Start()
    {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        Recorder.Close();
#endif
        _recordTimer = FindObjectOfType<SetRecorderTimer>();
    }

    void Update()
    {
        if (ShowProgressBar)
        {
            _recordTimer.Show();

            if (Playing)
            {
                if (ActivelyRecording)
                {
                    ElapsedVideoTime += TimeBetweenFrames;
                }
                else
                {
                    ElapsedVideoTime += Time.deltaTime;
                }

                _recordTimer.SetProgress(ElapsedVideoTime / RecordingTime);
            }
        }
        else
        {
            _recordTimer.Hide();
        }

        if (!ActivelyRecording)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Playing = !Playing;
            }
        }

        if (!Playing)
        {
            return;
        }

        if (ElapsedVideoTime > RecordingTime)
        {
            ElapsedVideoTime = 0;

            if (ActivelyRecording)
            {
                StopRecording();
                SceneManager.LoadScene("Scenes/Videos");
                SceneManager.SetActiveScene(SceneManager.GetSceneByName("Videos"));
            }

            return;
        }

        CaptureRecording();
    }

    private void CaptureRecording()
    {
        if (ActivelyRecording)
        {
            var camera = FindObjectOfType<Camera>();

            var originalSize = camera.orthographicSize;
            var originalPosition = camera.transform.position;

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


            var renderTexture = new RenderTexture(width, height, 24);

            camera.forceIntoRenderTexture = true;
            camera.targetTexture = renderTexture;
            camera.Render();

            RenderTexture.active = renderTexture;

            Texture2D lOut = new Texture2D(width, height, TextureFormat.ARGB32, false);
            lOut.ReadPixels(new Rect(0, 0, width, height), 0, 0);

            Frame++;
            var bytes = lOut.GetRawTextureData();

#if UNITY_STANDALONE_WIN || UNITY_EDITOR
            Recorder.SetFrameThroughBitMap(bytes, width, height);
#endif
            Destroy(lOut);
            camera.orthographicSize = originalSize;
            camera.transform.position = originalPosition;
            camera.forceIntoRenderTexture = false;
            camera.targetTexture = null;
            //imageTransform.Center();
            RenderTexture.active = null;

            Destroy(renderTexture);
        }
    }

    public void ResetRecordTime(int recordingTime)
    {
        if (Playing)
        {
            //StopRecordingMode();
        }

        ElapsedVideoTime = 0;
        RecordingTime = recordingTime;

        foreach (var item in FindObjectsOfType<RunningEffect>())
        {
            item.Length = RecordingTime;
            item.Initialize(RecordingTime);
        }
    }

    public void StartRecording()
    {
        var recorderMenuItem = FindObjectOfType<RecorderMenuItem>();
        var recordingTime = recorderMenuItem.GetSelectedRecordingTime();
        var format = recorderMenuItem.SelectedOutput;

        ResetRecordTime(recordingTime);

        ActivelyRecording = true;
        Playing = true;

        var videoFileName = recorderMenuItem.VideoFileName();

        var fps = recorderMenuItem.SelectedFrameRate;

        switch (fps)
        {
            case Fps.fps25:
                TimeBetweenFrames = 1.0f / 25.0f;
                break;
            case Fps.fps48:
                TimeBetweenFrames = 1.0f / 48.0f;
                break;
            case Fps.fps60:
                TimeBetweenFrames = 1.0f / 60.0f;
                break;
            default:
                break;
        }

#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        Recorder.Start(new Settings()
        {
            Fps = fps,
            InputImageFormat = InputImageFormat.Bmp,
            OutputFormat = format,
            OutputFile = videoFileName
        });
#endif

        AttributeMenuItem.RefreshButtonEnabledState();
    }

    public void StopRecording()
    {
        ElapsedVideoTime = 0;
        ActivelyRecording = false;
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        Recorder.Close();
#endif
        AttributeMenuItem.RefreshButtonEnabledState();
    }

    IEnumerator WaitAndClose()
    {
        yield return new WaitForSeconds(2);
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        Recorder.Close();
#endif
        AttributeMenuItem.RefreshButtonEnabledState();
    }

    internal void SetPercentage(float value)
    {
        ElapsedVideoTime = RecordingTime * value;

        foreach (var item in FindObjectsOfType<RunningEffect>())
        {
            item.Progress(ElapsedVideoTime);
        }
    }
}




