using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableParticleSystem : MonoBehaviour, IDragHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler
{
    public bool DragInProgress = false;
    public bool MouseHovered;
    private GameObject _assosicatedEffect;
    private Color SemiTransparent = new Color(1.0f, 1.0f, 1.0f, 0.5f);

    public void OnBeginDrag(PointerEventData eventData)
    {
        DragInProgress = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var mousePosition = Input.mousePosition;
        GetComponent<RectTransform>().position = mousePosition;

        var worldSpace = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldSpace.z = 0;

        _assosicatedEffect.transform.position = worldSpace;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragInProgress = false;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().color = MouseHovered ? Color.white : SemiTransparent;
    }

    internal void ConnectEffect(GameObject assosicatedEffect)
    {
        _assosicatedEffect = assosicatedEffect;

        var scenePoint = Camera.main.WorldToScreenPoint(_assosicatedEffect.transform.position);
        GetComponent<RectTransform>().position = scenePoint;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MouseHovered = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        MouseHovered = true;
    }
}
