using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DebugInput : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
#if DEBUG
        if (Input.GetKeyDown(KeyCode.U))
        {
            var uiGameObject = Resources.FindObjectsOfTypeAll<MainCanvas>().First().gameObject;
            var shown = uiGameObject.activeInHierarchy;
            uiGameObject.SetActive(!shown);
        }
#endif
    }
}
