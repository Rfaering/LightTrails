using System.IO;
using System.Linq;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    bool TakeScreenShot = false;

    // Use this for initialization
    void Start()
    {

        TakeScreenShot = false;

    }

    void OnPostRender()
    {

        if (TakeScreenShot)
        {
            var imagePicker = FindObjectOfType<ImageAreaPicker>();
            Rect rect = imagePicker.Center(512);
            int width = (int)rect.width;
            int height = (int)rect.height;

            Texture2D lOut = new Texture2D(width, height, TextureFormat.ARGB32, false);
            lOut.ReadPixels(rect, 0, 0);

            var effect = FindObjectsOfType<EffectMenuItem>().FirstOrDefault();
            var name = effect.gameObject.name ?? "test";

            File.WriteAllBytes(Application.dataPath + @"\Resources\Preview\" + name + ".png", lOut.EncodeToPNG());
            TakeScreenShot = false;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeScreenShot = true;
        }
    }
}
