using System;
using Assets.Models;
using UnityEngine.UI;
using UnityEngine;

public class SizeMenuItem : AttributeMenuItem
{
    public SizeAttribute SizeAttribute;

    public GameObject ExpandedWidth;
    public GameObject ExpandedHeight;
    public GameObject ExpandedX;
    public GameObject ExpandedY;

    public GameObject ClosedPosition;
    public GameObject ClosedSize;

    public GameObject Expand;
    public GameObject Closed;

    public bool Open = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Open = !Open;
        }

        GetComponent<LayoutElement>().minHeight = Open ? 180 : 80;
        Closed.SetActive(!Open);
        Expand.SetActive(Open);
    }

    internal void Initialize(SizeAttribute size)
    {
        var widthInput = ExpandedWidth.GetComponent<InputField>();
        var heightInput = ExpandedHeight.GetComponent<InputField>();

        var xInput = ExpandedX.GetComponent<InputField>();
        var yInput = ExpandedY.GetComponent<InputField>();

        GetComponentInChildren<Text>().text = size.Name;
        GetComponentInChildren<LinkButton>().IsLinked = size.IsLinked;

        xInput.onEndEdit.AddListener(newValue =>
        {
            int result;

            if (int.TryParse(newValue, out result))
            {
                size.X = result;
            }

            UpdateText();
        });

        yInput.onEndEdit.AddListener(newValue =>
        {
            int result;

            if (int.TryParse(newValue, out result))
            {
                size.Y = result;
            }

            UpdateText();
        });

        widthInput.onEndEdit.AddListener(newValue =>
        {
            int result;

            if (int.TryParse(newValue, out result))
            {
                size.Width = result;
            }

            UpdateText();
        });

        heightInput.onEndEdit.AddListener(newValue =>
        {
            int result;

            if (int.TryParse(newValue, out result))
            {
                size.Height = result;
            }

            UpdateText();
        });

        SizeAttribute = size;

        UpdateText();

        transform.Find("Text").GetComponent<Text>().text = size.Name;

        FindObjectOfType<FlexableFrame>().SetOffSet(size.X, size.Y);
        FindObjectOfType<FlexableFrame>().SetSize(size.Width, size.Height);
        /*_slider = slider;
        GetComponentInChildren<Text>().text = slider.Name;
        var sliderComponent = GetComponentInChildren<Slider>();
        sliderComponent.minValue = slider.Min;
        sliderComponent.maxValue = slider.Max;
        sliderComponent.value = slider.SelectedValue;
        sliderComponent.onValueChanged.AddListener(value => slider.Changed(value));*/
    }

    internal void SetOffSet(float x, float y)
    {
        SizeAttribute.SetOffSet(x, y);
        UpdateText();
    }

    internal void SetSize(float width, float height)
    {
        SizeAttribute.SetSize(width, height);
        UpdateText();
    }

    internal bool IsLinked()
    {
        return SizeAttribute.IsLinked;
    }

    internal void UpdateText()
    {
        int width = (int)(SizeAttribute.Width);
        int height = (int)(SizeAttribute.Height);

        int x = (int)(SizeAttribute.X);
        int y = (int)(SizeAttribute.Y);

        var widthInput = ExpandedWidth.GetComponent<InputField>();
        var heightInput = ExpandedHeight.GetComponent<InputField>();

        var xInput = ExpandedX.GetComponent<InputField>();
        var yInput = ExpandedY.GetComponent<InputField>();

        ClosedPosition.GetComponent<Text>().text = "Offset: " + x + " / " + y;
        ClosedSize.GetComponent<Text>().text = "Size: " + width + " / " + height;

        widthInput.text = width.ToString();
        heightInput.text = height.ToString();

        xInput.text = x.ToString();
        yInput.text = y.ToString();
    }

    internal void LinkedChangedValue(bool isLinked)
    {
        SizeAttribute.IsLinked = isLinked;
    }

    public override void ReEvaluateEnabled()
    {
        /*var slider = GetComponentInChildren<Slider>();

        if (_slider == null || slider == null)
        {
            return;
        }

        slider.interactable = _slider.IsEnabled();*/
    }
}
