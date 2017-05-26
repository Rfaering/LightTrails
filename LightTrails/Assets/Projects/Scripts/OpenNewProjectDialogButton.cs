using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class OpenNewProjectDialogButton : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OpenCreateButtonDialog);
    }

    public void OpenCreateButtonDialog()
    {
        Resources.FindObjectsOfTypeAll<NewProjectDialog>().First().Open();
    }

    internal void SetEnabled(bool enabled)
    {
        GetComponent<Button>().interactable = !enabled;
    }
}
