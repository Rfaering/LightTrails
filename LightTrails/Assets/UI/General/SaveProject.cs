using Assets.Projects.Scripts;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveProject : MonoBehaviour
{
    public Project Project;
    
    void Start()
    {
        if (Project.CurrentModel == null)
        {
            gameObject.SetActive(false);
            return;
        }

        Project = Project.CurrentModel;
        GetComponent<Button>().onClick.AddListener(SaveClicked);
    }

    private void SaveClicked()
    {
        var storedEffectState = new List<StoredParticleItem>();
        var storedImageState = new List<StoredImageItem>();

        var effectMenuItems = FindObjectsOfType<EffectMenuItem>();
        var imageMenuItem = FindObjectsOfType<ImageMenuItem>();

        foreach (var item in effectMenuItems)
        {
            storedEffectState.Add(item.GetEffectSaveState());
        }

        foreach (var item in imageMenuItem)
        {
            storedImageState.Add(item.GetImageSaveState());
        }

        Project.Items = new StoredItems()
        {
            Recorder = FindObjectOfType<RecorderMenuItem>().GetSaveState(),
            Images = storedImageState.ToArray(),
            Effects = storedEffectState.ToArray()
        };


        FindObjectOfType<ScreenShot>().TakeProjectThumbnail = true;

        Project.Save();

        GetComponent<Animation>().Play();
    }
}
