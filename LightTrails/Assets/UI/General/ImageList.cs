using UnityEngine;

public class ImageList : MonoBehaviour
{
    public GameObject ImagePrefab;

    public GameObject AddImage()
    {
        var image = Instantiate(ImagePrefab);
        image.transform.SetParent(transform);

        var siblingIndex = image.transform.GetSiblingIndex();

        image.transform.localPosition = new Vector3(0, 0, 3000 - 100 * siblingIndex);
        return image;
    }
}
