using UnityEngine;

public class MaskList : MonoBehaviour
{
    public GameObject MaskPrefab;

    // Use this for initialization
    void Start()
    {
        foreach (Transform child in transform)
        {
            DestroyObject(child.gameObject);
        }

        foreach (var item in MaskImages.Masks)
        {
            var newMaskButtom = Instantiate(MaskPrefab);
            newMaskButtom.GetComponent<MaskItem>().Initialize(item);
            newMaskButtom.transform.SetParent(transform);
            newMaskButtom.SetActive(true);
        }
    }
}
