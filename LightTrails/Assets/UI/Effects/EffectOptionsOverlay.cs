using UnityEngine;
using UnityEngine.EventSystems;

public class EffectOptionsOverlay : MonoBehaviour, IPointerClickHandler
{
    public GameObject Prefab;

    void Start()
    {
        foreach (var menuItem in GetComponentsInChildren<EffectOption>())
        {
            DestroyObject(menuItem.gameObject);
        }

        foreach (var effect in EffectOptions.Options)
        {
            var newGameObject = Instantiate(Prefab, transform);
            newGameObject.name = effect.Name;
            newGameObject.GetComponent<EffectOption>().Initialize(effect);
        }
    }

    internal void Close()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            FindObjectOfType<EffectOptionsOverlay>().Close();
        }
    }

    internal void Open()
    {
        gameObject.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        FindObjectOfType<EffectOptionsOverlay>().Close();
    }
}
