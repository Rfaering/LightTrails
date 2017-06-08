﻿using Assets.Projects.Scripts;
using Assets.UI.Models;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuItem : MonoBehaviour, IPointerClickHandler
{
    public bool Selected;

    private Color SelectedColor = new Color(0.8f, 1.0f, 0.8f);
    private Color DisabledColor = new Color(0.8f, 0.8f, 0.8f);
    private Color UnSelectedColor = Color.white;

    private bool IsDisabled { get { return FindObjectOfType<Record>().Recording; } }

    public Attribute[] Attributes;

    internal StoredItem GetSaveState()
    {
        return new StoredItem()
        {
            Attributes = new AttributesValues()
            {
                Values = Attributes
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

        var attributesByName = Attributes.ToDictionary(x => x.Name);

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
        HasBeenSelected();
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
}