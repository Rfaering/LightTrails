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

    // Use this for initialization
    void Start()
    {
        if (Attributes == null)
        {
            Attributes = GetAttributes().ToArray();
        }

        SelectEffect();
    }

    public virtual string GetName()
    {
        return string.Empty;
    }

    public virtual List<Attribute> GetAttributes()
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
        }.ToList();
    }

    public virtual void Initialize(Effect effect)
    {
    }

    internal void SetRotation(float value)
    {
        assosicatedEffect.transform.localRotation = Quaternion.Euler(0, 0, value);
    }

    internal void SetScale(float value)
    {
        assosicatedEffect.transform.localScale = new Vector3(value, value, value);
    }

    internal StoredEffectItem GetEffectSaveState()
    {
        return new StoredEffectItem()
        {
            Name = EffectName,
            Position = new[] { assosicatedEffect.transform.localPosition.x, assosicatedEffect.transform.localPosition.y, assosicatedEffect.transform.localPosition.z },
            Attributes = GetSaveState().Attributes
        };
    }

    public virtual void SetEffectSaveState(StoredEffectItem state)
    {
        Attributes = GetAttributes().ToArray();
        SetSaveState(state);
    }

    public virtual void Remove()
    {
        FindObjectOfType<ItemsMenu>().SelectFirstItem();
        Destroy(gameObject);
    }
}
