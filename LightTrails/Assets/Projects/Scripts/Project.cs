using System;
using System.IO;
#if UNITY_STANDALONE_WIN
using System.Runtime.Serialization.Formatters.Binary;
#elif UNITY_METRO
using System.Xml.Serialization;
#endif
using UnityEngine;

namespace Assets.Projects.Scripts
{
    [Serializable]
    public class Project
    {
        public static Project CurrentModel;

        public string Id;
        public string Name;
        public string OriginalImagePath;
        public StoredItems Items;

        public Project()
        {
            Items = new StoredItems();
        }

        public string ThumbnailPath()
        {
            var images = Path.Combine(GetProjectPath(), "images");
            var file = Path.Combine(images, "thumbnail.png");

            return file;
        }

        public void SaveThumbnail(byte[] bytes)
        {
            var thumbnailPath = ThumbnailPath();

            if (!Directory.Exists(thumbnailPath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(thumbnailPath));
            }

            if (!File.Exists(thumbnailPath))
            {
                var file = File.Create(thumbnailPath);
                file.Close();
            }

            File.WriteAllBytes(thumbnailPath, bytes);
        }

        internal string GetThumbnail()
        {
            return ThumbnailPath();
        }

        public string GetLocalImagePath()
        {
            if (string.IsNullOrEmpty(OriginalImagePath))
            {
                return null;
            }

            return Path.Combine(GetProjectPath(), "image" + Path.GetExtension(OriginalImagePath));
        }

        public string GetDataFilePath()
        {
            return Path.Combine(GetProjectPath(), "data");
        }

        public string GetClipFilePath(string extension)
        {
            var clipDirPath = GetClipDirectoryPath();

            var clipFilePath = Path.Combine(clipDirPath, Name + "." + extension);

            return clipFilePath;
        }

        public string GetClipDirectoryPath()
        {
            var clipDirPath = Path.Combine(GetProjectPath(), "clips");

            if (!Directory.Exists(clipDirPath))
            {
                Directory.CreateDirectory(clipDirPath);
            }

            return clipDirPath;
        }

        public string GetProjectPath()
        {
            return Path.Combine(PersistenceConsts.ProjectsDirectoryPath, Id);
        }

        public void Delete()
        {
            if (string.IsNullOrEmpty(Id))
            {
                Debug.LogError("Project should always have an id");
                return;
            }

            var projectDirectory = Path.Combine(PersistenceConsts.ProjectsDirectoryPath, Id);

            if (Directory.Exists(projectDirectory))
            {
                Directory.Delete(projectDirectory, true);
            }
        }

        public void Save()
        {
            if (string.IsNullOrEmpty(Id))
            {
                Debug.LogError("Project should always have an id");
                return;
            }

            var projectDirectory = GetProjectPath();

            if (!Directory.Exists(projectDirectory))
            {
                Directory.CreateDirectory(projectDirectory);
            }

            var localImage = GetLocalImagePath();

            if (!string.IsNullOrEmpty(localImage) && !File.Exists(localImage))
            {
                if (File.Exists(OriginalImagePath))
                {
                    File.Copy(OriginalImagePath, localImage);
                }
            }

            using (Stream stream = File.OpenWrite(GetDataFilePath()))
            {
#if UNITY_STANDALONE_WIN
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);
#elif UNITY_METRO
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Project));
                xmlSerializer.Serialize(stream, this);
#endif
            }
        }

        public static Project CreateFromPath(string directoryName)
        {
            if (!Directory.Exists(directoryName))
            {
                Debug.LogError("Can not load directory");
                return null;
            }

            var dataPath = Path.Combine(directoryName, "data");

            if (!File.Exists(dataPath))
            {
                Debug.LogError("Can not load data file");
                return null;
            }

            try
            {
                using (Stream stream = File.OpenRead(dataPath))
                {
#if UNITY_STANDALONE_WIN
                    BinaryFormatter formatter = new BinaryFormatter();
                    return formatter.Deserialize(stream) as Project;
#elif UNITY_METRO
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(Project));
                    return xmlSerializer.Deserialize(stream) as Project;
#endif
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }

            return null;
        }

        public static Project CreateNew(string name, string path)
        {
            return new Project()
            {
                Id = Guid.NewGuid().ToString(),
                OriginalImagePath = path,
                Name = name
            };
        }
    }
}
