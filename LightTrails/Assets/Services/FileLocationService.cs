using Assets.Projects.Scripts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public static class FileLocationService
{
    public static string VideoFileDirectory()
    {
        if (Project.CurrentModel != null)
        {
            return Project.CurrentModel.GetClipDirectoryPath();
        }

#if DEBUG
        return Path.GetFullPath(@"C:\Debug\Videos\");
#else
        return Path.GetFullPath(Directory.GetCurrentDirectory() + "\\");
#endif
    }
}
