using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Assets.Projects.Scripts;
using Assets.Models;
using System.Collections.Generic;

public class ParticleEffectMenuItem : EffectMenuItem
{
    public override void Initialize(Effect effect)
    {
        EffectName = effect.Name;
        GetComponentInChildren<Text>().text = effect.Name;
        var child = Resources.Load<GameObject>("Prefabs/" + effect.Name);
        if (child == null)
        {
            Debug.Log("Could not find prefab for particle system");
            return;
        }

        var createdEffect = Instantiate(child);
        createdEffect.transform.SetParent(FindObjectOfType<ActiveParticleList>().gameObject.transform);

        foreach (ParticleSystem ps in createdEffect.GetComponentsInChildren<ParticleSystem>())
        {
            ps.Clear();
            ps.Stop();
            ps.randomSeed = (uint)Random.Range(0, 10000);
            ps.Play();
        }

        createdEffect.gameObject.SetActive(true);

        assosicatedEffect = createdEffect.gameObject;
        createdEffect.transform.localPosition = new Vector3(0, 0, 10);

        base.Initialize(effect);
    }

    public void SetEffectBasedOnIndex(int index)
    {
        var localPostion = assosicatedEffect.transform.localPosition;
        assosicatedEffect.transform.localPosition = new Vector3(localPostion.x, localPostion.y, 30 - index);

        foreach (ParticleSystemRenderer ps in assosicatedEffect.GetComponentsInChildren<ParticleSystemRenderer>())
        {
            ps.material.renderQueue = 3000 - index * 100;
        }
    }

    public override Attribute[] GetAttributes()
    {
        var particleEffect = assosicatedEffect.GetComponent<ParticleEffect>();
        var result = new List<Attribute>();

        if (particleEffect.CanChangePosition)
        {
            result.Add(new SizeAttribute()
            {
                Name = "Area",
                Resizeable = true,
                X = particleEffect.X,
                Y = particleEffect.Y,
                Width = particleEffect.Size.x,
                Height = particleEffect.Size.y,
                OffSetChanged = values => particleEffect.SetPosition(values),
                SizeChanged = values => particleEffect.SetSize(values),
            });
        }

        return result.ToArray();
    }

    public override Rect GetRectOfAssociatedItem()
    {
        var particleEffect = assosicatedEffect.GetComponent<ParticleEffect>();
        var result = new Rect(0, 0, particleEffect.Size.x, particleEffect.Size.y);
        result.center = new Vector2(particleEffect.X, particleEffect.Y);
        return result;
    }

    public override void HasBeenSelected()
    {
        var draggableSystem = Resources.FindObjectsOfTypeAll<DraggableParticleSystem>().First();
        draggableSystem.ConnectEffect(assosicatedEffect);
        draggableSystem.gameObject.SetActive(true);

        base.HasBeenSelected();
    }

    public override void SetEffectSaveState(StoredParticleItem state)
    {
        assosicatedEffect.transform.localPosition = new Vector3(state.Position[0], state.Position[1], state.Position[2]);
        base.SetEffectSaveState(state);
    }

    public override void Remove()
    {
        Destroy(assosicatedEffect);
        base.Remove();
    }
}
