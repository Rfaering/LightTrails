
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ReorderableList : MonoBehaviour
{
    public LayoutGroup ContentLayout;
    public RectTransform DraggableArea;

    public bool IsDraggable = true;
    public bool IsDropable = true;

    [Header("UI Re-orderable Events")]
    public ReorderableListHandler OnElementDropped = new ReorderableListHandler();
    public ReorderableListHandler OnElementGrabbed = new ReorderableListHandler();

    private RectTransform _content;
    private ReorderableListContent _listContent;

    public RectTransform Content
    {
        get
        {
            if (_content == null)
            {
                _content = ContentLayout.GetComponent<RectTransform>();
            }
            return _content;
        }
    }

    Canvas GetCanvas()
    {
        return transform.GetComponentInParent<Canvas>();
    }

    private void Awake()
    {

        if (ContentLayout == null)
        {
            Debug.LogError("You need to have a child LayoutGroup content set for the list: " + name, gameObject);
            return;
        }
        if (DraggableArea == null)
        {
            DraggableArea = transform.root.GetComponentInChildren<Canvas>().GetComponent<RectTransform>();
        }
        if (IsDropable && !GetComponent<Graphic>())
        {
            Debug.LogError("You need to have a Graphic control (such as an Image) for the list [" + name + "] to be droppable", gameObject);
            return;
        }

        _listContent = ContentLayout.gameObject.AddComponent<ReorderableListContent>();
        _listContent.Init(this);
    }

    #region Nested type: ReorderableListEventStruct

    [Serializable]
    public struct ReorderableListEventStruct
    {
        public GameObject DroppedObject;
        public int FromIndex;
        public int ToIndex;
    }

    #endregion

    #region Nested type: ReorderableListHandler

    [Serializable]
    public class ReorderableListHandler : UnityEvent<ReorderableListEventStruct>
    {
    }

    public void ReorderAllItems(ReorderableListEventStruct item)
    {
        foreach (ImageMenuItem imageItem in FindObjectsOfType<ImageMenuItem>())
        {
            if (imageItem.gameObject == item.DroppedObject)
            {
                imageItem.UpdateImageLayer(item.ToIndex);
            }
            else
            {
                imageItem.UpdateImageLayer(imageItem.transform.GetSiblingIndex());
            }
        }

        foreach (ParticleEffectMenuItem particleMenuItem in FindObjectsOfType<ParticleEffectMenuItem>())
        {
            if (particleMenuItem.gameObject == item.DroppedObject)
            {
                particleMenuItem.SetEffectBasedOnIndex(item.ToIndex);
            }
            else
            {
                particleMenuItem.SetEffectBasedOnIndex(particleMenuItem.transform.GetSiblingIndex());
            }
        }
    }

    #endregion
}
