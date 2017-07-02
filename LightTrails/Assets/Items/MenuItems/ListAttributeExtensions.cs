using Assets.Models;
using System.Collections.Generic;
using System.Linq;

public static class ListAttributeExtensions
{
    public static void SetDefaultMaskValue(this List<Attribute> attributes, string value)
    {
        var maskAttribute = attributes.FirstOrDefault(x => x is MaskAttribute);
        if(maskAttribute != null)
        {
            (maskAttribute as MaskAttribute).SelectedValue = value;
        }
    }
}
