using Assets.Projects.Scripts;
using System.IO;
using System.Linq;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    public bool TakeScreenShot = false;
    public bool TakeProjectThumbnail = false;

    // Use this for initialization
    void Start()
    {

        TakeScreenShot = false;
    }

    void OnPostRender()
    {
        if (TakeProjectThumbnail)
        {
            TakeProjectThumbnail = false;
            var imagePicker = FindObjectOfType<RecorderAreaPicker>();
            Rect rect = imagePicker.Center(512);
            int width = (int)rect.width;
            int height = (int)rect.height;

            Texture2D lOut = new Texture2D(width, height, TextureFormat.ARGB32, false);
            lOut.ReadPixels(rect, 0, 0);

            Project.CurrentModel.SaveThumbnail(lOut.EncodeToPNG());
        }

        if (TakeScreenShot)
        {
            var imagePicker = FindObjectOfType<RecorderAreaPicker>();
            Rect rect = imagePicker.Center(512);
            int width = (int)rect.width;
            int height = (int)rect.height;

            Texture2D lOut = new Texture2D(width, height, TextureFormat.ARGB32, false);
            lOut.ReadPixels(rect, 0, 0);

            string name = null;

            var effect = FindObjectsOfType<EffectMenuItem>().FirstOrDefault();

            if (effect == null)
            {
                var imageMenuItem = FindObjectsOfType<ImageMenuItem>().FirstOrDefault();
                name = imageMenuItem.ShaderAttributes.Material.name;
            }
            else
            {
                name = effect.gameObject.name ?? "test";
            }

            File.WriteAllBytes(Application.dataPath + @"\Resources\Preview\" + name + ".png", lOut.EncodeToPNG());
            TakeScreenShot = false;

            Debug.Log("Screenshot taken! " + name);
        }
    }

    void Update()
    {
#if DEBUG
        EnabledSpace();
#endif
    }

    private void EnabledSpace()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeScreenShot = true;
        }
    }
}
