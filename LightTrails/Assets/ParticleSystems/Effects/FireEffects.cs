using UnityEngine;
using System;

public class FireEffects : MonoBehaviour
{
    public enum Color { Blue, Red, Green }

    public Color SelectedColor;

    private void Start()
    {
        SelectedColor = Color.Red;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform child in transform)
        {
            var shouldChildBeEnabled = child.gameObject.name == Enum.GetName(typeof(Color), SelectedColor);
            child.gameObject.SetActive(shouldChildBeEnabled);
        }
    }
}
