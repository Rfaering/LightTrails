using UnityEngine;
using Assets.Models;

public class AttributesMenu : MonoBehaviour
{
    public SliderMenuItem[] Existing;
    public GameObject SliderPrefab;
    public GameObject OptionsPrefab;
    public GameObject ActionPrefab;
    public GameObject TogglePrefab;
    public GameObject MaskPrefab;
    public GameObject SizePrefab;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateProperties(params Attribute[] attributes)
    {
        foreach (Transform child in transform)
        {
            DestroyObject(child.gameObject);
        }

        foreach (var attribute in attributes ?? new SliderAttribute[0])
        {
            if (attribute is SliderAttribute)
            {
                var newSlider = Instantiate(SliderPrefab);
                newSlider.GetComponent<SliderMenuItem>().Initialize(attribute as SliderAttribute);
                newSlider.transform.SetParent(transform);
            }
            else if (attribute is MaskAttribute)
            {
                var newMask = Instantiate(MaskPrefab);
                newMask.GetComponent<MaskMenuItem>().Initialize(attribute as MaskAttribute);
                newMask.transform.SetParent(transform);
            }
            else if (attribute is SizeAttribute)
            {
                var newMask = Instantiate(SizePrefab);
                newMask.GetComponent<SizeMenuItem>().Initialize(attribute as SizeAttribute);
                newMask.transform.SetParent(transform);
            }
            else if (attribute is OptionsAttribute)
            {
                var newOptions = Instantiate(OptionsPrefab);
                newOptions.GetComponent<OptionsMenuItem>().Initialize(attribute as OptionsAttribute);
                newOptions.transform.SetParent(transform);
            }
            else if (attribute is ActionAttribute)
            {
                var newAction = Instantiate(ActionPrefab);
                newAction.GetComponent<ActionMenuItem>().Initialize(attribute as ActionAttribute);
                newAction.transform.SetParent(transform);
            }
            else if (attribute is ToggleAttribute)
            {
                var newAction = Instantiate(TogglePrefab);
                newAction.GetComponent<ToggleMenuItem>().Initialize(attribute as ToggleAttribute);
                newAction.transform.SetParent(transform);
            }
        }
    }
}

