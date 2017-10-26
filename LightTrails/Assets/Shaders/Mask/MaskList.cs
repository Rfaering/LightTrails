using UnityEngine;

public class MaskList : MonoBehaviour
{
    public GameObject MaskPrefab;

    public bool HasBeenLoaded = false;

    // Use this for initialization
    void Awake()
    {
        EnsureLoaded();
    }

    public void EnsureLoaded()
    {
        if (HasBeenLoaded)
        {
            return;
        }

        foreach (Transform child in transform)
        {
            DestroyObject(child.gameObject);
        }

        foreach (var item in MaskImages.AllMasks)
        {
            var newMaskButtom = Instantiate(MaskPrefab);
            newMaskButtom.GetComponent<MaskItem>().Initialize(item);
            newMaskButtom.transform.SetParent(transform);
            newMaskButtom.SetActive(true);
        }

        HasBeenLoaded = true;
    }

    public void AddMask(Texture2D texture)
    {
        var newMaskButtom = Instantiate(MaskPrefab);
        newMaskButtom.GetComponent<MaskItem>().Initialize(texture);
        newMaskButtom.transform.SetParent(transform);
        newMaskButtom.SetActive(true);
    }
}
