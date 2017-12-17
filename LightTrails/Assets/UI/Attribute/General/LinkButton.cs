using System;
using UnityEngine;
using UnityEngine.UI;

public class LinkButton : MonoBehaviour
{
    public Texture2D Linked;
    public Texture2D UnLinked;

    public bool IsLinked = true;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Callback);
    }

    private void Callback()
    {
        IsLinked = !IsLinked;
        GetComponentInParent<SizeMenuItem>().LinkedChangedValue(IsLinked);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsLinked)
        {
            GetComponentInChildren<RawImage>().texture = Linked;
        }
        else
        {
            GetComponentInChildren<RawImage>().texture = UnLinked;
        }
    }
}
