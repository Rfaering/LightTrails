using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Projects.Scripts
{
    public static class PersistenceConsts
    {
        private static string projectsFolder = "Projects";
        public static string ProjectsDirectoryPath = Path.Combine(Application.persistentDataPath, projectsFolder);
    }
}
