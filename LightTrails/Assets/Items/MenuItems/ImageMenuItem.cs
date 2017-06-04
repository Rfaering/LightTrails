using Assets.Projects.Scripts;
using Assets.UI.Models;

public class ImageMenuItem : MenuItem
{
    private ImageProperties ImageProperties;

    void Awake()
    {
        ImageProperties = FindObjectOfType<ImageProperties>();

        Attributes = new Attribute[]
        {
            new ActionAttribute()
            {
                Name = "Open Image",
                Action = () => FindImage()
            },
            new SliderAttribute()
            {
                Name = "Brightness",
                CallBack = UpdateLighting,
                SelectedValue = ImageProperties.Lighting,
                Min = 10
            },
            new SliderAttribute()
            {
                Name = "Scale",
                CallBack = UpdateScale,
                SelectedValue = ImageProperties.Scale,
                Min = 10
            },
            /*new ToggleAttribute()
            {
                Name = "Light",
                CallBack = value => FindObjectOfType<LightPlane>().SetEnabled(value)
            }*/
        };

        if (Project.CurrentModel != null)
        {
            SetSaveState(Project.CurrentModel.Items.Image);
        }
    }

    public void UpdateLighting(float value)
    {
        ImageProperties.Lighting = value;
    }

    public void UpdateTransparency(float value)
    {
        ImageProperties.Transparency = value;
    }

    public void UpdateScale(float value)
    {
        ImageProperties.Scale = value;
    }

    public void FindImage()
    {
        StandaloneFileBrowser.OpenFilePanel(callBack =>
       {
           FindObjectOfType<ImageProperties>().SetImage(callBack);
       });
    }
}
