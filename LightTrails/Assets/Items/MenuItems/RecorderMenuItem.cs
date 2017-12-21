using Assets.Models;
using Assets.Projects.Scripts;
using System;
using System.Collections.Generic;
#if UNITY_STANDALONE_WIN
using System.Diagnostics;
using System.Globalization;
#endif
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RecorderMenuItem : MenuItem
{
    public const string Time5Secs = "5 seconds";
    public const string Time10Secs = "10 seconds";
    public const string Time20Secs = "20 seconds";
    public const string Time30Secs = "30 seconds";
    public const string Time60Secs = "60 seconds";

    public string SelectedSeconds = Time10Secs;
    public FfmpegWrapper.OutputFormat SelectedOutput = FfmpegWrapper.OutputFormat.AVI;
    public FfmpegWrapper.Fps SelectedFrameRate = FfmpegWrapper.Fps.fps25;

    public Record _record;

    void Awake()
    {
        _record = FindObjectOfType<Record>();

        if (Project.CurrentModel != null)
        {
            SetSaveState(Project.CurrentModel.Items.Recorder);
        }
    }

    public override Assets.Models.Attribute[] GetAttributes()
    {
        var areaPicker = FindObjectOfType<RecorderAreaPicker>();

        var attributes = new List<Assets.Models.Attribute>()
        {
           new SizeAttribute()
           {
               Name = "Recorded Area",
               Resizeable = true,
               X = areaPicker.X,
               Y = areaPicker.Y,
               ForceWidth = areaPicker.Width,
               ForceHeight = areaPicker.Height,
               SizeChanged = values => areaPicker.SetSize(values),
               OffSetChanged= values => areaPicker.SetOffSet(values)           
           },
           new OptionsAttribute<FfmpegWrapper.OutputFormat>()
           {
               Name = "Output Type",
               SpecificCallBack = newSelection =>
               {
                   SelectedOutput = newSelection;
                   AttributeMenuItem.RefreshButtonEnabledState();
               },
               SpecificSelectedValue = SelectedOutput,
               IsEnabled = () => !_record.ActivelyRecording
           },
           new OptionsAttribute<FfmpegWrapper.Fps>()
           {
               Name = "Frame Rate",
               SpecificCallBack = newSelection =>
               {
                   SelectedFrameRate = newSelection;
                   AttributeMenuItem.RefreshButtonEnabledState();
               },
               SpecificSelectedValue = SelectedFrameRate,
               IsEnabled = () => !_record.ActivelyRecording
           },
           new OptionsAttribute()
           {
               Name = "Time",
               Options = new List<string>
               { Time10Secs, Time20Secs, Time30Secs, Time60Secs },
               SelectedValue = SelectedSeconds,
               CallBack = newSelection => {
                   SelectedSeconds = newSelection;
                   FindObjectOfType<Record>().ResetRecordTime(GetSelectedRecordingTime());
               },
               IsEnabled = () => !_record.ActivelyRecording
           },
           /*new ActionAttribute()
           {
               Name = "Open Video",
               Action = () => { OpenFile(VideoFileName()); },
               IsEnabled = () => FileOpenEnabled(VideoFileName())
           },*/
           /*new ActionAttribute()
           {
               Name = "Open Website",
               Action = () => { OpenFile(HtmlFileName()); },
               IsEnabled = () => FileOpenEnabled(HtmlFileName())
           },*/
           new ActionAttribute()
            {
                Name = "Videos",
                Action = () => { OpenVideoGrid(); /*OpenFile(VideoFileDirectory());*/ }
            }
    };
        return attributes.ToArray();
    }

    public override void HasBeenSelected()
    {
        //FindObjectOfType<Record>().ResetRecordTime(GetSelectedRecordingTime());
        base.HasBeenSelected();
    }

    public override void HasBeenUnSelected()
    {
        //FindObjectOfType<Record>().StopRecordingMode();
        base.HasBeenUnSelected();
    }

    public int GetSelectedRecordingTime()
    {
        switch (SelectedSeconds)
        {
            case Time5Secs: { return 5; }
            case Time10Secs: { return 10; }
            case Time20Secs: { return 20; }
            case Time30Secs: { return 30; }
            case Time60Secs: { return 60; }
            default:
                break;
        }

        return 0;
    }

    public void OpenVideoGrid()
    {
        SceneManager.LoadScene("Scenes/Videos", LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Videos"));
    }

    public void OpenFile(string filePath)
    {
        var fullPath = Path.GetFullPath(filePath);
#if UNITY_STANDALONE_WIN
        Process.Start(fullPath);
#endif
    }

    public string VideoFileName()
    {
        var extension = Enum.GetName(typeof(FfmpegWrapper.OutputFormat), SelectedOutput);
        var value = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture);
        if (Project.CurrentModel != null)
        {
            return Project.CurrentModel.GetClipFilePath(value, extension);
        }

#if DEBUG
        return Path.GetFullPath(@"C:\Debug\Videos\" + value + "." + extension);
#else
        return Path.GetFullPath(Directory.GetCurrentDirectory() + "\\test." + Enum.GetName(typeof(FfmpegWrapper.OutputFormat), SelectedOutput));
#endif
    }

    private string HtmlFileName()
    {
#if DEBUG
        return Path.GetFullPath(Directory.GetCurrentDirectory() + "\\..\\Websites\\Demo\\index.html");
#else
        return Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Websites\\Demo\\index.html");
#endif
    }

    private bool FileOpenEnabled(string file)
    {
        return !_record.ActivelyRecording && File.Exists(VideoFileName());
    }

    public override Rect GetRectOfAssociatedItem()
    {
        var areaPicker = FindObjectOfType<RecorderAreaPicker>();
        return new Rect(areaPicker.X, areaPicker.Y, areaPicker.Width, areaPicker.Height);
    }
}
