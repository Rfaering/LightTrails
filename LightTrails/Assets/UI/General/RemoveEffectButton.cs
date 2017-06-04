using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RemoveEffectButton : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(RemoveEffect);
    }

    private void RemoveEffect()
    {
        GetComponentInParent<EffectMenuItem>().Remove();
    }
}
