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

        GetComponent<LayoutElement>().minHeight = Open ? 180 : 100;
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
        //GetComponentInChildren<LinkButton>().IsLinked = size.IsLinked;

        xInput.onEndEdit.AddListener(newValue =>
        {
            int result;

            if (int.TryParse(newValue, out result))
            {
                size.X = result;
            }

            UpdateText();
            UpdateFrameOffset();
        });

        yInput.onEndEdit.AddListener(newValue =>
        {
            int result;

            if (int.TryParse(newValue, out result))
            {
                size.Y = result;
            }

            UpdateText();
            UpdateFrameOffset();
        });

        widthInput.interactable = size.Resizeable;
        heightInput.interactable = size.Resizeable;

        widthInput.onEndEdit.AddListener(newValue =>
        {
            int result;

            if (int.TryParse(newValue, out result))
            {
                size.Width = result;
            }

            UpdateText();
            UpdateFrameSize();
        });

        heightInput.onEndEdit.AddListener(newValue =>
        {
            int result;

            if (int.TryParse(newValue, out result))
            {
                size.Height = result;
            }

            UpdateText();
            UpdateFrameSize();
        });

        SizeAttribute = size;

        UpdateText();

        transform.Find("Text").GetComponent<Text>().text = size.Name;

        FindObjectOfType<FlexableFrame>().SetOffSet(size.X, size.Y);
        FindObjectOfType<FlexableFrame>().SetSize(size.Width, size.Height);

        Closed.SetActive(!Open);
        Expand.SetActive(Open);

        var shrinkButton = GetComponentInChildren<ShrinkButton>(true);
        shrinkButton.gameObject.SetActive(FindObjectOfType<RecorderMenuItem>().Selected);

    }

    internal void SetOffSet(float x, float y)
    {
        SizeAttribute.SetOffSet(x, y);
        UpdateText();
        UpdateFrameOffset();
    }

    internal void SetSize(float width, float height)
    {
        SizeAttribute.SetSize(width, height);
        UpdateText();
        UpdateFrameSize();
    }

    internal void UpdateFrameSize()
    {
        FindObjectOfType<FlexableFrame>().SetSize(SizeAttribute.Width, SizeAttribute.Height);
    }

    internal void UpdateFrameOffset()
    {
        FindObjectOfType<FlexableFrame>().SetOffSet(SizeAttribute.X, SizeAttribute.Y);
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

        ClosedPosition.GetComponentInChildren<Text>().text = x + " / " + y;
        ClosedSize.GetComponentInChildren<Text>().text = width + " / " + height;

        widthInput.text = width.ToString();
        heightInput.text = height.ToString();

        xInput.text = x.ToString();
        yInput.text = y.ToString();
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
