using Assets.Models;
using UnityEngine;
using UnityEngine.UI;

public class EffectOptionImageLoader : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    public void SetImage(Effect effect)
    {
        var parentName = gameObject.transform.parent.gameObject.name;
        var loadedImage = Resources.Load<Texture2D>("Preview/" + parentName);
        if (loadedImage != null)
        {
            GetComponent<Image>().sprite = Sprite.Create(loadedImage, new Rect(0, 0, loadedImage.width, loadedImage.height), Vector2.zero);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
