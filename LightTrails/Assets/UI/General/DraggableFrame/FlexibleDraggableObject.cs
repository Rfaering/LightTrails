using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class FlexibleDraggableObject : MonoBehaviour
{
    public GameObject Target;
    private EventTrigger _eventTrigger;
    void Start()
    {
        _eventTrigger = GetComponent<EventTrigger>();
        _eventTrigger.AddEventTrigger(OnDrag, EventTriggerType.Drag);
    }

    void OnDrag(BaseEventData data)
    {
        var zoomFactor = FindObjectOfType<Zoom>().ZoomFactor;

        var sizeMenuItem = FindObjectOfType<SizeMenuItem>();

        PointerEventData ped = (PointerEventData)data;

        var rect = Target.GetComponent<RectTransform>();

        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);

        rect.anchoredPosition = new Vector2(sizeMenuItem.SizeAttribute.X, sizeMenuItem.SizeAttribute.Y);

        Target.transform.Translate(ped.delta / zoomFactor);

        GetComponentInParent<FlexableFrame>().OffsetUpdated();
    }
}