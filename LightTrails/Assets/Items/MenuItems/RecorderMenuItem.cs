using Assets.Projects.Scripts;
using Assets.UI.Models;
using System;
using System.Collections.Generic;
#if UNITY_STANDALONE_WIN
using System.Diagnostics;
#endif
using System.IO;
using System.Linq;

public class RecorderMenuItem : MenuItem
{
    public const string Time5Secs = "5 seconds";
    public const string Time10Secs = "10 seconds";
    public const string Time20Secs = "20 seconds";
    public const string Time30Secs = "30 seconds";
    public const string Time60Secs = "60 seconds";

    public string SelectedSeconds = Time10Secs;
    public FfmpegWrapper.OutputFormat SelectedOutput = FfmpegWrapper.OutputFormat.AVI;
    public FfmpegWrapper.Fps SelectedFrameRate = FfmpegWrapper.Fps.fps24;

    public Record _record;

    void Awake()
    {
        _record = FindObjectOfType<Record>();

        var attributes = new List<Assets.UI.Models.Attribute>()
        {
           new ActionAttribute()
           {
               Name = "Begin",
               Action = () => { StartRecording(); },
               IsEnabled = () => !_record.Recording
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
               IsEnabled = () => !_record.Recording
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
               IsEnabled = () => !_record.Recording
           },
           new OptionsAttribute()
           {
               Name = "Time",
               Options = new List<string>
               { Time10Secs, Time20Secs, Time30Secs, Time60Secs },
               SelectedValue = SelectedSeconds,
               CallBack = newSelection => { SelectedSeconds = newSelection; },
               IsEnabled = () => !_record.Recording
           },

           new ActionAttribute()
           {
               Name = "Open Video",
               Action = () => { OpenFile(VideoFileName()); },
               IsEnabled = () => FileOpenEnabled(VideoFileName())
           },
           /*new ActionAttribute()
           {
               Name = "Open Website",
               Action = () => { OpenFile(HtmlFileName()); },
               IsEnabled = () => FileOpenEnabled(HtmlFileName())
           },*/
           new ActionAttribute()
            {
                Name = "Open Directory",
                Action = () => { OpenFile(VideoFileDirectory()); }
            }
    };

        Attributes = attributes.ToArray();

        if (Project.CurrentModel != null)
        {
            SetSaveState(Project.CurrentModel.Items.Recorder);
        }
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

    public void StartRecording()
    {
        int value = GetSelectedRecordingTime();
        FindObjectOfType<Record>().StartRecording(value, SelectedOutput);
    }

    public void OpenFile(string filePath)
    {
        var fullPath = Path.GetFullPath(filePath);
#if UNITY_STANDALONE_WIN
        Process.Start(fullPath);
#endif
    }

    public string VideoFileDirectory()
    {
        if (Project.CurrentModel != null)
        {
            return Project.CurrentModel.GetClipDirectoryPath();
        }

#if DEBUG
        return Path.GetFullPath(Directory.GetCurrentDirectory() + "\\..\\");
#else
        return Path.GetFullPath(Directory.GetCurrentDirectory() + "\\");
#endif
    }

    public string VideoFileName()
    {
        var extension = Enum.GetName(typeof(FfmpegWrapper.OutputFormat), SelectedOutput);
        if (Project.CurrentModel != null)
        {
            return Project.CurrentModel.GetClipFilePath(extension);
        }

#if DEBUG
        return Path.GetFullPath(Directory.GetCurrentDirectory() + "\\..\\test." + extension);
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
        return !_record.Recording && File.Exists(VideoFileName());
    }
}
