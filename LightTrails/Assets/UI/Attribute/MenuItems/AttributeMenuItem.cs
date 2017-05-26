using UnityEngine;

public class AttributeMenuItem : MonoBehaviour
{

    public virtual void ReEvaluateEnabled()
    {

    }

    public static void RefreshButtonEnabledState()
    {
        foreach (var item in FindObjectsOfType<AttributeMenuItem>())
        {
            item.ReEvaluateEnabled();
        }
    }
}
