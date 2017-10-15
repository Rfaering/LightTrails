using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Assets.Projects.Scripts;
using Assets.Models;

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
