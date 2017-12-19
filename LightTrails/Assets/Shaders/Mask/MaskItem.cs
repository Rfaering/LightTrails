using UnityEngine;
using UnityEngine.UI;

public class MaskItem : MonoBehaviour
{
    public bool Selected;
    public Color SelectedColor;
    public Color NotSelectedColor;

    public Texture2D Texture;

    public string Name { get { return Texture.name; } }

    // Use this for initialization
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        Select();
    }

    public void Select()
    {
        foreach (MaskItem maskItem in Resources.FindObjectsOfTypeAll<MaskItem>())
        {
            maskItem.Selected = false;
        }

        Selected = true;
        
        var maskMenuItem = FindObjectOfType<MaskMenuItem>();
        if (maskMenuItem != null)
        {
            maskMenuItem.SetSelection(Name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Selected)
        {
            transform.GetChild(0).GetComponent<Image>().color = SelectedColor;
        }
        else
        {
            transform.GetChild(0).GetComponent<Image>().color = NotSelectedColor;
        }
    }

    internal void Initialize(Texture2D item)
    {
        Texture = item;
        GetComponentInChildren<RawImage>().texture = item;
    }
}

