﻿public interface IStandaloneFileBrowser
{
    string[] OpenFilePanel(string title, string directory, ExtensionFilter[] extensions, bool multiselect);
    string[] OpenFolderPanel(string title, string directory, bool multiselect);
    string SaveFilePanel(string title, string directory, string defaultName, ExtensionFilter[] extensions);
}

public struct ExtensionFilter
{
    public string Name;
    public string[] Extensions;

    public ExtensionFilter(string filterName, params string[] filterExtensions)
    {
        Name = filterName;
        Extensions = filterExtensions;
    }
}