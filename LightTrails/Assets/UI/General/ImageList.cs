using UnityEngine;

public class ImageList : MonoBehaviour
{
    public GameObject ImagePrefab;

    public GameObject AddImage()
    {
        var image = Instantiate(ImagePrefab);
        image.transform.SetParent(transform);

        image.transform.localPosition = new Vector3(0, 0);

        return image;
    }
}
