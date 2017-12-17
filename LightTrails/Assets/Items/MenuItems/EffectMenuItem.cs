using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Assets.Projects.Scripts;
using Assets.Models;

public class EffectMenuItem : MenuItem
{
    public GameObject assosicatedEffect;
    public bool IsLooping;

    public string EffectName;

    public virtual string GetName()
    {
        return string.Empty;
    }

    public override Attribute[] GetAttributes()
    {
        return new Attribute[]
        {
            /*new SliderAttribute()
            {
                Name = "Scale",
                Min = 1.0f,
                Max = 3.0f,
                SelectedValue = 1,
                CallBack = SetScale
            },*/
            /*new SliderAttribute()
            {
                Name = "Rotation",
                Min = -180,
                Max = 180,
                SelectedValue = 0,
                CallBack = SetRotation
            }*/
        };
    }

    public virtual void Initialize(Effect effect)
    {
        var record = FindObjectOfType<Record>();
        assosicatedEffect.GetComponent<RunningEffect>()
            .Initialize(record.RecordingTime);
    }

    internal void SetRotation(float value)
    {
        assosicatedEffect.transform.localRotation = Quaternion.Euler(0, 0, value);
    }

    internal void SetScale(float value)
    {
        assosicatedEffect.transform.localScale = new Vector3(value, value, value);
    }

    internal StoredParticleItem GetEffectSaveState()
    {
        return new StoredParticleItem()
        {
            Name = EffectName,
            Position = new[] { assosicatedEffect.transform.localPosition.x, assosicatedEffect.transform.localPosition.y, assosicatedEffect.transform.localPosition.z },
            Attributes = GetSaveState().Attributes,
            Index = transform.GetSiblingIndex()
        };
    }

    public virtual void SetEffectSaveState(StoredParticleItem state)
    {
        SetSaveState(state);
    }

    public override Rect GetRectOfAssociatedItem()
    {
        return Rect.zero;
    }
}
