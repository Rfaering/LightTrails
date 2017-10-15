using UnityEngine;
using UnityEngine.UI;

public class RemoveEffectButton : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(RemoveEffect);
    }

    private void RemoveEffect()
    {
        GetComponentInParent<MenuItem>().Remove();
    }
}
