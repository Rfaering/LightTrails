using UnityEngine;
using UnityEngine.EventSystems;

public class EffectOptionsOverlay : MonoBehaviour, IPointerClickHandler
{
    internal void Close()
    {
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
        MaskPanel.Close();
        gameObject.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        FindObjectOfType<EffectOptionsOverlay>().Close();
    }
}
