using Assets.Projects.Scripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveProject : MonoBehaviour
{
    private Project _project;

    void Start()
    {
        if (Project.CurrentModel == null)
        {
            gameObject.SetActive(false);
            return;
        }

        _project = Project.CurrentModel;
        GetComponent<Button>().onClick.AddListener(SaveClicked);
    }

    private void SaveClicked()
    {
        var effects = new List<StoredEffectItem>();
        var effectMenuItems = FindObjectsOfType<EffectMenuItem>();

        foreach (var item in effectMenuItems)
        {
            effects.Add(item.GetEffectSaveState());
        }

        _project.Items = new StoredItems()
        {
            Recorder = FindObjectOfType<RecorderMenuItem>().GetSaveState(),
            Image = FindObjectOfType<ImageMenuItem>().GetSaveState(),
            Effects = effects.ToArray()
        };

        _project.Save();

        GetComponent<Animation>().Play();
    }
}
