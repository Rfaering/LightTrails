using Assets.UI.Models;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using Assets.Projects.Scripts;

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

    internal StoredEffectItem GetEffectSaveState()
    {
        return new StoredEffectItem()
        {
            Name = EffectName,
            Position = new[] { assosicatedEffect.transform.localPosition.x, assosicatedEffect.transform.localPosition.y, assosicatedEffect.transform.localPosition.z },
            Attributes = GetSaveState().Attributes
        };
    }

    internal void SetEffectSaveState(StoredEffectItem state)
    {
        Attributes = GetAttributes().ToArray();
        assosicatedEffect.transform.localPosition = new Vector3(state.Position[0], state.Position[1], state.Position[2]);
        SetSaveState(state);
    }

    internal void SetScale(float value)
    {
        var scalable = assosicatedEffect.GetComponent<ScalableParticleSystem>();
        if (scalable != null)
        {
            scalable.gameObject.transform.localScale = new Vector3(value, value, value);
        }

        var children = assosicatedEffect.GetComponentsInChildren<ScalableParticleSystem>(true);
        foreach (var item in children)
        {
            item.gameObject.transform.localScale = new Vector3(value, value, value);
        }
    }

    internal void SetRotation(float value)
    {
        assosicatedEffect.transform.localRotation = Quaternion.Euler(0, 0, value);
    }

    internal void Initialize(Effect effect)
    {
        EffectName = effect.Name;
        GetComponentInChildren<Text>().text = effect.Name;
        var child = Resources.FindObjectsOfTypeAll<ParticleList>().First().gameObject.transform.Find(effect.Name);
        var createdEffect = Instantiate(child, FindObjectOfType<ActiveParticleList>().gameObject.transform);
        createdEffect.gameObject.SetActive(true);

        assosicatedEffect = createdEffect.gameObject;
    }

    public override void HasBeenSelected()
    {
        var draggableSystem = Resources.FindObjectsOfTypeAll<DraggableParticleSystem>().First();
        draggableSystem.ConnectEffect(assosicatedEffect);
        draggableSystem.gameObject.SetActive(true);
    }

    internal void Remove()
    {
        FindObjectOfType<ItemsMenu>().SelectFirstItem();
        Destroy(assosicatedEffect);
        Destroy(gameObject);
    }
}
