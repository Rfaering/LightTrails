using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewProjectDialog : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Close();
    }

    internal void SetProjectName(string name)
    {
        GetComponentInChildren<InputField>().text = name;
    }

    internal string GetProjectName()
    {
        return GetComponentInChildren<InputField>().text;
    }

    internal void Close()
    {
        gameObject.SetActive(false);
    }

    internal void Open()
    {
        SetProjectName(string.Empty);
        //GetComponentInChildren<NewProjectImagePicker>().SetDefaultImage();
        gameObject.SetActive(true);
    }
}
