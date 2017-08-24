using FfmpegWrapper;
using UnityEngine;
using System.Collections;

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

    private void OnPreRender()
    {
        if (!ActivelyRecording)
        {
            return;
        }
    }

    void OnPostRender()
    {
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
            }
            return;
        }

        if (ActivelyRecording)
        {
            var imagePicker = FindObjectOfType<ImageAreaPicker>();
            Rect rect = imagePicker.GetRect();
            int width = (int)rect.width;
            int height = (int)rect.height;

            Texture2D lOut = new Texture2D(width, height, TextureFormat.ARGB32, false);
            lOut.ReadPixels(rect, 0, 0);

            Frame++;
            var bytes = lOut.GetRawTextureData();

#if UNITY_STANDALONE_WIN || UNITY_EDITOR
            Recorder.SetFrameThroughBitMap(bytes, width, height);
#endif
            Destroy(lOut);
        }
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




