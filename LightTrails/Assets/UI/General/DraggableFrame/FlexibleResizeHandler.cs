using System;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public enum HandlerType
{
    TopRight, Right, BottomRight, Bottom, BottomLeft, Left, TopLeft, Top
}

[RequireComponent(typeof(EventTrigger))]
public class FlexibleResizeHandler : MonoBehaviour
{
    public HandlerType Type;
    public RectTransform Target;
    public Vector2 MinimumDimmensions = new Vector2(50, 50);
    public Vector2 MaximumDimmensions = new Vector2(4000, 4000);

    private EventTrigger _eventTrigger;

    void Start()
    {
        _eventTrigger = GetComponent<EventTrigger>();
        _eventTrigger.AddEventTrigger(OnDrag, EventTriggerType.Drag);
    }

    void OnDrag(BaseEventData data)
    {
        PointerEventData ped = (PointerEventData)data;

        RectTransform.Edge? horizontalEdge = null;
        RectTransform.Edge? verticalEdge = null;

        switch (Type)
        {
            case HandlerType.TopRight:
                horizontalEdge = RectTransform.Edge.Left;
                verticalEdge = RectTransform.Edge.Bottom;
                break;
            case HandlerType.Right:
                horizontalEdge = RectTransform.Edge.Left;
                break;
            case HandlerType.BottomRight:
                horizontalEdge = RectTransform.Edge.Left;
                verticalEdge = RectTransform.Edge.Top;
                break;
            case HandlerType.Bottom:
                verticalEdge = RectTransform.Edge.Top;
                break;
            case HandlerType.BottomLeft:
                horizontalEdge = RectTransform.Edge.Right;
                verticalEdge = RectTransform.Edge.Top;
                break;
            case HandlerType.Left:
                horizontalEdge = RectTransform.Edge.Right;
                break;
            case HandlerType.TopLeft:
                horizontalEdge = RectTransform.Edge.Right;
                verticalEdge = RectTransform.Edge.Bottom;
                break;
            case HandlerType.Top:
                verticalEdge = RectTransform.Edge.Bottom;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (horizontalEdge != null)
        {
            Target.sizeDelta = Target.sizeDelta + new Vector2(2 * ped.delta.x * (horizontalEdge == RectTransform.Edge.Right ? -1 : 1), 0);
        }
        if (verticalEdge != null)
        {
            Target.sizeDelta = Target.sizeDelta + new Vector2(0, 2 * ped.delta.y * (verticalEdge == RectTransform.Edge.Top ? -1 : 1));
        }

        Target.sizeDelta = Vector2.Max(MinimumDimmensions, Target.sizeDelta);
        Target.sizeDelta = Vector2.Min(MaximumDimmensions, Target.sizeDelta);

        GetComponentInParent<FlexableFrame>().SizeUpdated();
    }
}
