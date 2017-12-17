using Assets.Models;
using Assets.Projects.Scripts;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class MenuItem : MonoBehaviour, IPointerClickHandler
{
    public bool Selected { get; private set; }

    private Color SelectedColor = Color.white;
    private Color DisabledColor = new Color(0.5f, 0.5f, 0.5f);
    private Color UnSelectedColor = new Color(0.6f, 0.6f, 0.6f);

    private bool IsDisabled { get { return FindObjectOfType<Record>().ActivelyRecording; } }

    public abstract Attribute[] GetAttributes();
    public abstract Rect GetRectOfAssociatedItem();

    internal StoredItem GetSaveState()
    {
        return new StoredItem()
        {
            Attributes = new AttributesValues()
            {
                Values = GetAttributes()
                .Select(x => x.GetAttributeValue())
                .Where(x => x != null)
                .ToArray()
            }
        };
    }

    internal void SetSaveState(StoredItem item)
    {
        if (item == null || item.Attributes == null || item.Attributes.Values == null)
        {
            return;
        }

        var attributesByName = GetAttributes().ToDictionary(x => x.Name);

        foreach (var value in item.Attributes.Values)
        {
            if (attributesByName.ContainsKey(value.Key))
            {
                attributesByName[value.Key].SetAttributeValue(value);
            }
        }
    }

    public virtual void HasBeenSelected()
    {
        Selected = true;
    }

    public virtual void HasBeenUnSelected()
    {
        Selected = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SelectEffect();
    }

    public void SelectEffect()
    {
        if (IsDisabled)
        {
            return;
        }

        FindObjectOfType<ItemsMenu>().ItemSelected(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDisabled)
        {
            GetComponent<Image>().color = DisabledColor;
        }
        else if (Selected)
        {
            GetComponent<Image>().color = SelectedColor;
        }
        else
        {
            GetComponent<Image>().color = UnSelectedColor;
        }
    }

    public virtual void Remove()
    {
        FindObjectOfType<ItemsMenu>().SelectFirstItem();
        Destroy(gameObject);
    }
}
