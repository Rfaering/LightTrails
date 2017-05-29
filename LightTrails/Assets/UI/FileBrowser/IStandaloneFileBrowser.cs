using System;

public interface IStandaloneFileBrowser
{
    void OpenFilePanel(Action<string> callBack);
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