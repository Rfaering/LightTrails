using System;
using UnityEngine;

public class Notifications : MonoBehaviour
{
    private Action _callBack;

    public void PlaySaveNotification(Action callBack)
    {
        _callBack = callBack;
        var animation = GetComponent<Animation>();

        animation.Play();
    }

    public void NotificationDone()
    {
        if (_callBack != null)
        {
            _callBack();
            _callBack = null;
        }
    }
}
