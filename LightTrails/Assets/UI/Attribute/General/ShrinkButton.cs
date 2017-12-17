using System;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ShrinkButton : MonoBehaviour
{
    void Start()
    {
        GetComponentInChildren<Button>().onClick.AddListener(ClickHandler);
    }

    private void ClickHandler()
    {
        var nonSelectedItems = FindObjectsOfType<MenuItem>()
                                    .Where(x => !x.Selected)
                                    .Select(x => x.GetRectOfAssociatedItem())
                                    .Where(x => x != Rect.zero)
                                    .ToArray();

        Rect[] rects = nonSelectedItems;
        if (rects.Any())
        {
            var overlappingRect = rects.Aggregate((one, two) => Rect.MinMaxRect(Math.Min(one.xMin, two.xMin), Math.Min(one.yMin, two.yMin), Math.Max(one.xMax, two.xMax), Math.Max(one.yMax, two.yMax)));

            var sizeMenuItem = GetComponentInParent<SizeMenuItem>();
            sizeMenuItem.SetSize(overlappingRect.width, overlappingRect.height);
            sizeMenuItem.SetOffSet(overlappingRect.center.x, overlappingRect.center.y);
        }
    }
}
