using Assets.Projects.Scripts;
using System;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoItem : MonoBehaviour
{
    private VideoFileInfo _videoFileInfo;

    void Start()
    {
        transform.Find("Delete").GetComponent<Button>().onClick.AddListener(Delete);
    }

    private void Delete()
    {
        if (Project.CurrentModel != null)
        {
            if (Project.CurrentModel.DeleteClip(Path.GetFileNameWithoutExtension(_videoFileInfo.Location)))
            {
                Destroy(gameObject);
            }
        }
    }

    internal void SetVideoInfo(VideoFileInfo videoFileInfo)
    {
        _videoFileInfo = videoFileInfo;
        transform.Find("Type/Value").GetComponent<Text>().text = videoFileInfo.Type.Replace(".", "").ToLowerInvariant();
        transform.Find("Size/Value").GetComponent<Text>().text = SizeSuffix(videoFileInfo.Length);

        GetComponent<Button>().onClick.AddListener(StartPlaying);
    }

    public void StartPlaying()
    {
        var fullPath = Path.GetFullPath(_videoFileInfo.Location);
#if UNITY_STANDALONE_WIN
        Process.Start(fullPath);
#endif

        /*var renderTexture = new RenderTexture(200, 200, 24);
        var rawImage = _player.GetComponent<RawImage>();
        rawImage.texture = renderTexture;

        rawImage.enabled = true;

        var videoPlayer = _player.GetComponent<VideoPlayer>();
        videoPlayer.targetTexture = renderTexture;
        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = _videoFileInfo.Location;

        videoPlayer.Play();*/
    }

    static readonly string[] SizeSuffixes =
                   { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

    static string SizeSuffix(Int64 value, int decimalPlaces = 1)
    {
        if (decimalPlaces < 0) { throw new ArgumentOutOfRangeException("decimalPlaces"); }
        if (value < 0) { return "-" + SizeSuffix(-value); }
        if (value == 0) { return string.Format("{0:n" + decimalPlaces + "} bytes", 0); }

        int mag = (int)Math.Log(value, 1024);

        decimal adjustedSize = (decimal)value / (1L << (mag * 10));

        if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
        {
            mag += 1;
            adjustedSize /= 1024;
        }

        return string.Format("{0:n" + decimalPlaces + "} {1}",
            adjustedSize,
            SizeSuffixes[mag]);
    }
}
