using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SizeExpandOnClick : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        var menuItem = GetComponentInParent<SizeMenuItem>();
        menuItem.Open = !menuItem.Open;
    }
}
