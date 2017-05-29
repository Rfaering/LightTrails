
using System;

public class StandaloneFileBrowser
{
    private static IStandaloneFileBrowser _platformWrapper = null;

    static StandaloneFileBrowser()
    {
#if UNITY_STANDALONE_OSX
            _platformWrapper = new StandaloneFileBrowserMac();
#elif UNITY_STANDALONE_WIN
        _platformWrapper = new StandaloneFileBrowserWindows();
#elif UNITY_METRO
        _platformWrapper = new StandaloneFileBrowserMetro();
#elif UNITY_EDITOR
        _platformWrapper = new StandaloneFileBrowserEditor();
#endif
    }

    /// <summary>
    /// Native open file dialog
    /// </summary>
    /// <param name="title">Dialog title</param>
    /// <param name="directory">Root directory</param>
    /// <param name="extensions">List of extension filters. Filter Example: new ExtensionFilter("Image Files", "jpg", "png")</param>
    /// <param name="multiselect">Allow multiple file selection</param>
    /// <returns>Returns array of chosen paths. Zero length array when cancelled</returns>
    public static void OpenFilePanel(Action<string> callback)
    {
        _platformWrapper.OpenFilePanel(callback);
    }

}