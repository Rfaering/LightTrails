using Assets.Models;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EffectOptionsOverlay : MonoBehaviour, IPointerClickHandler
{
    public static Effect.EffectKind? RenderedEffectType;

    internal void Close()
    {
        /*foreach (var item in GetComponentsInChildren<EffectContentContainer>(true))
        {
            item.gameObject.transform.parent.gameObject.SetActive(true);
        }

        FindObjectOfType<Record>().ShowProgressBar = true;
        gameObject.SetActive(false);*/
        SceneManager.UnloadSceneAsync("Scenes/Effects");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FindObjectOfType<EffectOptionsOverlay>().Close();
        }
    }

    void Start()
    {
        if (RenderedEffectType != null)
        {
            foreach (var item in GetComponentsInChildren<EffectContentContainer>())
            {
                item.gameObject.transform.parent.gameObject.SetActive(item.EffectKind == RenderedEffectType.Value);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        FindObjectOfType<EffectOptionsOverlay>().Close();
    }
}
