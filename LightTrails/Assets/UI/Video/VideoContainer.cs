using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class VideoContainer : MonoBehaviour
{
    public GameObject Prefab;

    public void Initialize()
    {
        var videoPath = FileLocationService.VideoFileDirectory();

        foreach (Transform subcomponents in transform)
        {
            Destroy(subcomponents.gameObject);
        }

        foreach (var file in Directory.GetFiles(videoPath))
        {
            FileInfo fileInfo = new FileInfo(file);

            var videoFileInfo = new VideoFileInfo()
            {
                Type = fileInfo.Extension,
                Length = fileInfo.Length,
                Location = file
            };

            var newElement = Instantiate(Prefab);
            newElement.GetComponent<VideoItem>().SetVideoInfo(videoFileInfo);
            newElement.transform.SetParent(transform);
        }
    }
}
