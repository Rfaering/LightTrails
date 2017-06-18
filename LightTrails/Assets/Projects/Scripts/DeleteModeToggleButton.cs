using System;
using UnityEngine;
using UnityEngine.UI;

public class DeleteModeToggleButton : MonoBehaviour
{
    public bool DeleteMode;
    // Use this for initialization
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ToggleMode);
        SetButtonStates();
    }

    private void ToggleMode()
    {
        DeleteMode = !DeleteMode;
        SetButtonStates();
    }

    private void SetButtonStates()
    {
        if (DeleteMode)
        {
            GetComponentInChildren<RawImage>().color = Color.red;
        }
        else
        {
            GetComponentInChildren<RawImage>().color = Color.white;
        }

        FindObjectOfType<OpenNewProjectDialogButton>().SetEnabled(DeleteMode);
    }
}
