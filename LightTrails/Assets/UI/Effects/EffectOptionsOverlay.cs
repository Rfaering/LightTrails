using UnityEngine;
using UnityEngine.EventSystems;

public class EffectOptionsOverlay : MonoBehaviour, IPointerClickHandler
{
    internal void Close()
    {
        FindObjectOfType<Record>().ShowProgressBar = true;
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FindObjectOfType<EffectOptionsOverlay>().Close();
        }
    }

    internal void Open()
    {
        FindObjectOfType<Record>().ShowProgressBar = false;
        MaskPanel.Close();
        gameObject.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        FindObjectOfType<EffectOptionsOverlay>().Close();
    }
}
