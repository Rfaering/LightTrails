using Assets.Projects.Scripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveProject : MonoBehaviour
{
    public Button _button;

    void Start()
    {
        /*if (Project.CurrentModel == null)
        {
            gameObject.SetActive(false);
            return;
        }*/

        _button = GetComponent<Button>();
        _button.onClick.AddListener(SaveClicked);
    }

    private void SaveClicked()
    {
        if (Project.CurrentModel == null)
        {
            return;
        }

        _button.interactable = false;

        FindObjectOfType<Notifications>().PlaySaveNotification(() => _button.interactable = true);

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

        Project.CurrentModel.Items = new StoredItems()
        {
            Recorder = FindObjectOfType<RecorderMenuItem>().GetSaveState(),
            Images = storedImageState.ToArray(),
            Effects = storedEffectState.ToArray()
        };


        FindObjectOfType<ScreenShot>().TakeProjectThumbnail = true;

        Project.CurrentModel.Save();

        //GetComponent<Animation>().Play();
    }
}
