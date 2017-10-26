using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tab : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        var input = GetComponent<InputField>();
        if (Input.GetKeyDown(KeyCode.Tab) && input.isFocused)
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                input.navigation.selectOnUp.Select();
            }
            else
            {
                input.navigation.selectOnDown.Select();
            }
        }
    }
}
