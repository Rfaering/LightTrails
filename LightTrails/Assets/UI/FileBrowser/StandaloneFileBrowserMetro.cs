#if UNITY_METRO
using System;
using Windows10Port;

public class StandaloneFileBrowserMetro : IStandaloneFileBrowser
{
    public void OpenFilePanel(Action<string> callBack)
    {
        OpenFile.ChooseFile(file => UnityMainThreadDispatcher.Enqueue(() => { callBack(file); }));
    }
}

#endif