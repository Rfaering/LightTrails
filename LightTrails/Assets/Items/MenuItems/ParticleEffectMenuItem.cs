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

        createdEffect.gameObject.SetActive(true);

        assosicatedEffect = createdEffect.gameObject;
    }

    public override void HasBeenSelected()
    {
        var draggableSystem = Resources.FindObjectsOfTypeAll<DraggableParticleSystem>().First();
        draggableSystem.ConnectEffect(assosicatedEffect);
        draggableSystem.gameObject.SetActive(true);
    }

    public override void SetEffectSaveState(StoredEffectItem state)
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
