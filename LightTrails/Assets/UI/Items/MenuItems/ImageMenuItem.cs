using Assets.Projects.Scripts;
using Assets.UI.Models;
using System.Linq;

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
            /*new SliderAttribute()
            {
                Name = "Transparency",
                Changed = UpdateTransparency,
                DefaultValue = (int)ImageProperties.Transparency,
                Min = 10
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
        // Open file with filter
        var extensions = new[] {
            new ExtensionFilter("Image Files", "png", "jpg", "jpeg" ),
        };
        var paths = StandaloneFileBrowser.OpenFilePanel("Open File", System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyPictures), extensions, true);

        if (paths != null && paths.Length > 0)
        {
            FindObjectOfType<ImageProperties>().SetImage(paths.First());
        }
    }
}
