using FfmpegWrapper;
using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public partial class Record : MonoBehaviour
{
    public bool ActivelyRecording = false;
    public bool RecordMode = false;

    public float TimeBetweenFrames = 1.0f / 25.0f;

    public float ElapsedVideoTime = 0;
    private float RecordingTime = 10;

    public bool FirstFrame = false;

    public GameObject ProgressBar;

    public int Frame = 0;

    public List<IRecordedEffect> RecordedEffects = new List<IRecordedEffect>();

    void Start()
    {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        Recorder.Close();
#endif
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
        if (!ActivelyRecording)
        {
            return;
        }

        if (FirstFrame)
        {
            Frame = 0;
            FirstFrame = false;
        }
        else
        {
            ElapsedVideoTime += TimeBetweenFrames;
            foreach (var ps in RecordedEffects)
            {
                ps.Progress(ElapsedVideoTime);
            }
        }

        if (ElapsedVideoTime > RecordingTime)
        {
            StopRecording();
            return;
        }

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

    public void StopRecordingMode()
    {
        if (!RecordMode)
        {
            return;
        }

        foreach (var ps in RecordedEffects)
        {
            ps.Destroy();
        }

        RecordedEffects.Clear();
        RecordMode = false;
    }

    void Update()
    {
        if (RecordMode)
        {
            var videoProgressbar = ProgressBar.GetComponent<VideoProgressBar>();
            videoProgressbar.Show();

            if (ActivelyRecording)
            {
                videoProgressbar.SetProgress((ElapsedVideoTime / RecordingTime));
            }
        }
        else
        {
            ProgressBar.GetComponent<VideoProgressBar>().Hide();
        }
    }

    public void PrepareRecordMode(int recordingTime)
    {
        if (RecordMode)
        {
            StopRecordingMode();
        }

        FirstFrame = true;
        ElapsedVideoTime = 0;
        RecordingTime = recordingTime;

        var recorderMenuItem = FindObjectOfType<RecorderMenuItem>();

        var activeParticleList = FindObjectOfType<ActiveParticleList>().gameObject;

        var allParticleSystems = activeParticleList
                                    .GetComponentsInChildren<ParticleSystem>()
                                    .Where(x => x.transform.parent == activeParticleList.transform);

        var particleEffects = allParticleSystems.Select(ps => new RecordedParticleSystem(ps, RecordingTime)).ToArray();

        var shaderEffects = FindObjectsOfType<ShaderEffect>()
            .Select(shaderEffect => new RecordedShader(shaderEffect, RecordingTime)).ToArray();

        RecordedEffects.AddRange(particleEffects);
        RecordedEffects.AddRange(shaderEffects);

        RecordMode = true;
    }

    public void StartRecording(int recordingTime, OutputFormat format)
    {
        PrepareRecordMode(recordingTime);

        ActivelyRecording = true;

        var recorderMenuItem = FindObjectOfType<RecorderMenuItem>();
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
        foreach (var ps in RecordedEffects)
        {
            ps.Progress(ElapsedVideoTime);
        }
    }
}




